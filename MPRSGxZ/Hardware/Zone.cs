﻿using MPRSGxZ.Commands;
using MPRSGxZ.Enumerators;
using MPRSGxZ.Events;
using System;
using System.Collections.Generic;

namespace MPRSGxZ.Hardware
{
	public class Zone
	{
		//
		// Amp IDs are the identifier that the firware uses for each amp (1, 2, or 3)
		// Zone IDs are the identifier that the firmware uses for each zone (1-6)
		//
		// For example:
		// Amp 1 Zone 1 = 11
		// Amp 1 Zone 5 = 15
		// Amp 2 Zone 1 = 21
		// Amp 2 Zone 6 = 26
		// and so on
		//
		public int AmpID { get; private set; }
		public int ZoneID { get; private set; }

		//
		// Amplifier properties stored in the amp firmware
		//
		private bool _Power;
		public bool Power
		{
			get
			{
				return _Power;
			}
			set
			{
				if (_Power != value)
				{
					_Power = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Power, AmpID, ZoneID, value ? 1 : 0)));
				}
			}
		}

		private bool _Mute;
		public bool Mute
		{
			get
			{
				return _Mute;
			}
			set
			{
				if (_Mute != value)
				{
					_Mute = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Mute, AmpID, ZoneID, value ? 1 : 0)));
				}
			}
		}

		private bool _PublicAddress;
		public bool PublicAddress
		{
			get
			{
				return _PublicAddress;
			}
			set
			{
				if (_PublicAddress != value)
				{
					_PublicAddress = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.PublicAddress, AmpID, ZoneID, value ? 1 : 0)));
				}
			}
		}

		private bool _DoNotDisturb;
		public bool DoNotDisturb
		{
			get
			{
				return _DoNotDisturb;
			}
			set
			{
				if (_DoNotDisturb != value)
				{
					_DoNotDisturb = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.DoNotDisturb, AmpID, ZoneID, value ? 1 : 0)));
				}
			}
		}

		private int _Volume;
		public int Volume
		{
			get
			{
				return _Volume;
			}
			set
			{
				if (_Volume != value)
				{
					if (value > Command.Volume.MaxValue)
					{
						value = Command.Volume.MaxValue;
					}
					else if (value < Command.Volume.MinValue)
					{
						value = Command.Volume.MinValue;
					}

					_Volume = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Volume, AmpID, ZoneID, value)));
				}
			}
		}

		private int _Treble;
		public int Treble
		{
			get
			{
				return _Treble;
			}
			set
			{
				if (_Treble != value)
				{
					if (value > Command.Treble.MaxValue)
					{
						value = Command.Treble.MaxValue;
					}
					else if (value < Command.Treble.MinValue)
					{
						value = Command.Treble.MinValue;
					}

					_Treble = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Treble, AmpID, ZoneID, value)));
				}
			}
		}

		private int _Bass;
		public int Bass
		{
			get
			{
				return _Bass;
			}
			set
			{
				if (_Bass != value)
				{
					if (value > Command.Bass.MaxValue)
					{
						value = Command.Bass.MaxValue;
					}
					else if (value < Command.Bass.MinValue)
					{
						value = Command.Bass.MinValue;
					}

					_Bass = value;
					QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Treble, AmpID, ZoneID, value)));
				}
			}
		}

		private int _Balance;
		public int Balance
		{
			get
			{
				return _Balance;
			}
			set
			{
				if (value > Command.Balance.MaxValue)
				{
					value = Command.Balance.MaxValue;
				}
				else if (value < Command.Balance.MinValue)
				{
					value = Command.Balance.MinValue;
				}

				_Balance = value;
				QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Bass, AmpID, ZoneID, value)));
			}
		}

		private int _Source;
		public int Source
		{
			get
			{
				return _Source;
			}
			set
			{
				if (value > Command.Source.MaxValue)
				{
					value = Command.Source.MaxValue;
				}
				else if (value < Command.Source.MinValue)
				{
					value = Command.Source.MinValue;
				}

				_Source = value;
				QueueCommand?.Invoke(new QueueCommandEventArgs(new Command(BaseCommand.Source, AmpID, ZoneID, value)));
			}
		}

		//
		// Amplifier properties stored in software
		//
		public string Name { get; set; }
		public bool Enabled { get; set; }
		private ZoneLinkMode m_LinkStatus;
		private List<int> m_LinkedZones;
		private decimal m_VolumeFactor;

		private event QueueCommandEvent QueueCommand;
		private event ZoneChangedEvent ZoneChanged;

		/// <summary>
		/// Default constructor for missing configuration
		/// </summary>
		/// <param name="AmpID">The ID of the amplifier this zone is a member of</param>
		/// <param name="AmpZoneID">The ID of this zone internal to the owning amplifier</param>
		internal Zone(int AmpID, int ZoneID, QueueCommandEvent QueueCommand, ZoneChangedEvent ZoneChanged)
		{
			this.AmpID = AmpID;
			this.ZoneID = ZoneID;
			this.Enabled = true;
			this.Name = $"Amp {AmpID} Zone {ZoneID}";
			this.QueueCommand = QueueCommand;
			this.ZoneChanged = ZoneChanged;

			m_VolumeFactor = 1;
			m_LinkStatus = ZoneLinkMode.Unlinked;
			m_LinkedZones = new List<int>();
		}

		/// <summary>
		/// Attaches events for the zone
		/// </summary>
		/// <param name="SettingChanged">The handler for when Zone software settings are changed</param>
		/// <param name="QueueCommand">The handler to queue a command with the AmplifierPort when Zone hardware settings are changed</param>
		internal void AttachEvents(QueueCommandEvent QueueCommand, ZoneChangedEvent ZoneChanged)
		{
			this.QueueCommand = QueueCommand;
			this.ZoneChanged = ZoneChanged;
		}

		/// <summary>
		/// A value which is multiplied with the volume set for this zone to produce the adjusted volume. Values less than 0 will be set to 0.1 and greater than 3 will be set to 3
		/// </summary>
		public decimal VolumeFactor
		{
			get
			{
				return m_VolumeFactor;
			}
			set
			{
				if(m_VolumeFactor != value)
				{
					//
					// Volume factor cant be negative (obviously) or zero (as this zone would never turn on).
					// Technically we could have the volume factor go as high as Command.Volume.MaxValue,
					// a set volume of 0 would result in an adjusted volume of 0 and a set volume of anything
					// else would result in the adjusted volume being maxed.
					//
					// The maximum value of 5 an artificial limitation for the sake of sanity 
					//
					if (value <= 0 || value > 5)
					{
						throw new ArgumentOutOfRangeException();
					}
					else
					{
						m_VolumeFactor = value;
						//SettingChangedEvent?.Invoke();
					}
				}
			}
		}

		/// <summary>
		/// The calculated volume for this zone based off of the volume factor
		/// </summary>
#warning Make readonly in C# 8
		public int AdjustedVolume
		{
			get
			{
				return Convert.ToInt32(m_VolumeFactor * _Volume);
			}
		}

		internal void UpdateState(bool PublicAddress, bool Power, bool Mute, bool DoNotDisturb, int Volume, int Treble, int Bass, int Balance, int Source)
		{
			bool ValueChanged = false;

			if(_PublicAddress != PublicAddress)
			{
				_PublicAddress = PublicAddress;
				ValueChanged = true;
			}
			
			if(_Power != Power)
			{
				_Power = Power;
				ValueChanged = true;
			}

			if (_Mute != Mute)
			{
				_Mute = Mute;
				ValueChanged = true;
			}

			if (_DoNotDisturb != DoNotDisturb)
			{
				_DoNotDisturb = DoNotDisturb;
				ValueChanged = true;
			}

			if (_Volume != Volume)
			{
				_Volume = Volume;
				ValueChanged = true;
			}

			if (_Treble != Treble)
			{
				_Treble = Treble;
				ValueChanged = true;
			}

			if (_Bass != Bass)
			{
				_Bass = Bass;
				ValueChanged = true;
			}

			if (_Balance != Balance)
			{
				_Balance = Balance;
				ValueChanged = true;
			}

			if (_Source != Source)
			{
				_Source = Source;
				ValueChanged = true;
			}
			
			if(ValueChanged)
			{
				ZoneChanged?.Invoke(new ZoneChangedEventArgs(AmpID, ZoneID));
			}
		}
	}
}