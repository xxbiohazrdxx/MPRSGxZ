using System;
using System.Runtime.Serialization;
using MPRSGxZ.Events;
using System.Collections.Generic;
using MPRSGxZ.Exceptions;

namespace MPRSGxZ
{
	[DataContract]
	public class Zone
	{
		//
		// The firmware handles IDs slightly differently, it uses the Amp ID and the ZoneID
		// For example:
		// Amp 1 Zone 1 = 11
		// Amp 1 Zone 5 = 15
		// Amp 2 Zone 1 = 21
		// Amp 2 Zone 6 = 26
		// and so on
		//

		//
		// Amp IDs are the identifier that the firware uses for each amp (1, 2, or 3)
		//
		private int m_AmpID;

		//
		// Zone IDs are the identifier that the firmware uses for each zone (1-6)
		//
		private int m_ZoneID;

		//
		// Amplifier properties stored in the amp hardware
		//
		private bool m_Power;
		private bool m_Mute;
		private bool m_PublicAddress;
		private bool m_DoNotDisturb;

		private int m_Volume;
		private int m_Treble;
		private int m_Bass;
		private int m_Balance;
		private int m_Source;

		//
		// Amplifier properties stored in software
		//
		private string m_Name;
		private bool m_Enabled;
		private ZoneLinkMode m_LinkStatus;
		private List<int> m_LinkedZones;
		private decimal m_VolumeFactor;

		private event QueueCommandEventHandler QueueCommandEvent;
		private event SettingChangedEventHandler SettingChangedEvent;
		private event ZoneChangedEventHandler ZoneChangedEvent;

		private bool Initialized;

		/// <summary>
		/// Default blank constructor used for deserialization
		/// </summary>
		internal Zone()
		{
		}

		/// <summary>
		/// Default constructor for missing configuration
		/// </summary>
		/// <param name="AmpID">The ID of the amplifier this zone is a member of</param>
		/// <param name="AmpZoneID">The ID of this zone internal to the owning amplifier</param>
		internal Zone(int AmpID, int ZoneID)
		{
			m_AmpID = AmpID;
			m_ZoneID = ZoneID;

			m_Enabled = false;
			m_Name = string.Format(@"Amp {0} Zone {1}", AmpID, ZoneID);

			m_VolumeFactor = 1;
			m_LinkStatus = ZoneLinkMode.Unlinked;
			m_LinkedZones = new List<int>();
		}

		/// <summary>
		/// Attaches events for the zone
		/// </summary>
		/// <param name="SettingChanged">The handler for when Zone software settings are changed</param>
		/// <param name="QueueCommand">The handler to queue a command with the AmplifierPort when Zone hardware settings are changed</param>
		internal void AttachEvents(SettingChangedEventHandler SettingChanged, QueueCommandEventHandler QueueCommand, ZoneChangedEventHandler ZoneChanged)
		{
			SettingChangedEvent = SettingChanged;
			QueueCommandEvent = QueueCommand;
			ZoneChangedEvent = ZoneChanged;
		}

		//
		// Properties that are stored by the software
		//
		[IgnoreDataMember]
		internal int AmpID
		{
			get
			{
				return m_AmpID;
			}
			set
			{
				m_AmpID = value;
			}
		}

		[DataMember]
		public int ZoneID
		{
			get
			{
				return m_ZoneID;
			}
			internal set
			{
				m_ZoneID = value;
			}
		}

		[DataMember]
		public string Name
		{
			get
			{
				return m_Name;
			}
			set
			{
				m_Name = value;
				SettingChangedEvent?.Invoke();
			}
		}

		[DataMember]
		public bool Enabled
		{
			get
			{
				return m_Enabled;
			}
			set
			{
				m_Enabled = value;
				SettingChangedEvent?.Invoke();
			}
		}

		[DataMember]
		public ZoneLinkMode LinkStatus
		{
			get
			{
				return m_LinkStatus;
			}
			internal set
			{
				m_LinkStatus = value;
				SettingChangedEvent?.Invoke();
			}
		}

		[DataMember]
		public List<int> LinkedZones
		{
			get
			{
				return m_LinkedZones;
			}
			internal set
			{
				m_LinkedZones = value;
				SettingChangedEvent?.Invoke();
			}
		}

		/// <summary>
		/// A value which is multiplied with the volume set for this zone to produce the adjusted volume. Values less than 0 will be set to 0.1 and greater than 3 will be set to 3
		/// </summary>
		[DataMember]
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
						SettingChangedEvent?.Invoke();
					}
				}
			}
		}

		/// <summary>
		/// The calculated volume for this zone based off of the volume factor
		/// </summary>
		[IgnoreDataMember]
		public int AdjustedVolume
		{
			get
			{
				return Convert.ToInt32(m_VolumeFactor * m_Volume);
			}
		}

		//
		// Properties that are stored by the hardware
		//
		[IgnoreDataMember]
		public bool Power
		{
			get
			{
				return m_Power;
			}
			set
			{
				if (m_Power != value)
				{
					m_Power = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Power, Convert.ToInt32(value)));
				}
			}
		}

		[IgnoreDataMember]
		public bool Mute
		{
			get
			{
				return m_Mute;
			}
			set
			{
				if (m_Mute != value)
				{
					m_Mute = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Mute, Convert.ToInt32(value)));
				}
			}
		}

		[IgnoreDataMember]
		public bool PublicAddress
		{
			get
			{
				return m_PublicAddress;
			}
			set
			{
				if (m_PublicAddress != value)
				{
					m_PublicAddress = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.PublicAddress, Convert.ToInt32(value)));
				}
			}
		}

		[IgnoreDataMember]
		public bool DoNotDisturb
		{
			get
			{
				return m_DoNotDisturb;
			}
			set
			{
				if (m_DoNotDisturb != value)
				{
					m_DoNotDisturb = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.DoNotDisturb, Convert.ToInt32(value)));
				}
			}
		}

		[IgnoreDataMember]
		public int Volume
		{
			get
			{
				return m_Volume;
			}
			set
			{
				if (m_Volume != value)
				{
					if (value > Command.Volume.MaxValue)
					{
						value = Command.Volume.MaxValue;
					}
					else if (value < Command.Volume.MinValue)
					{
						value = Command.Volume.MinValue;
					}

					m_Volume = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Volume, value));
				}
			}
		}

		[IgnoreDataMember]
		public int Treble
		{
			get
			{
				return m_Treble;
			}
			set
			{
				if (m_Treble != value)
				{
					if (value > Command.Treble.MaxValue)
					{
						value = Command.Treble.MaxValue;
					}
					else if (value < Command.Treble.MinValue)
					{
						value = Command.Treble.MinValue;
					}

					m_Treble = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Treble, value));
				}
			}
		}

		[IgnoreDataMember]
		public int Bass
		{
			get
			{
				return m_Bass;
			}
			set
			{
				if (m_Bass != value)
				{
					if (value > Command.Bass.MaxValue)
					{
						value = Command.Bass.MaxValue;
					}
					else if (value < Command.Bass.MinValue)
					{
						value = Command.Bass.MinValue;
					}

					m_Bass = value;
					QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Treble, value));
				}
			}
		}

		[IgnoreDataMember]
		public int Balance
		{
			get
			{
				return m_Balance;
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

				m_Balance = value;
				QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Bass, value));
			}
		}

		[IgnoreDataMember]
		public int Source
		{
			get
			{
				return m_Source;
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

				m_Source = value;
				QueueCommandEvent?.Invoke(new QueueCommandEventArgs(m_AmpID, m_ZoneID, Command.Source, value));
			}
		}

		/// <summary>
		/// Processes the polling data and, if a hardware property has changed, updates it and the ZonePropertyChangedEvent
		/// </summary>
		/// <param name="e">The ZonePropertyPollEventArgs for this poll result</param>
		internal void UpdateFromPollData(ZonePollEventArgs e)
		{
			bool ValueChanged = false;

			if(m_PublicAddress != e.PublicAddress)
			{
				m_PublicAddress = e.PublicAddress;
				ValueChanged = true;
			}
			
			if(m_Power != e.Power)
			{
				m_Power = e.Power;
				ValueChanged = true;
			}

			if (m_Mute != e.Mute)
			{
				m_Mute = e.Mute;
				ValueChanged = true;
			}

			if (m_DoNotDisturb != e.DoNotDisturb)
			{
				m_DoNotDisturb = e.DoNotDisturb;
				ValueChanged = true;
			}

			if (m_Volume != e.Volume)
			{
				m_Volume = e.Volume;
				ValueChanged = true;
			}

			if (m_Treble != e.Treble)
			{
				m_Treble = e.Treble;
				ValueChanged = true;
			}

			if (m_Bass != e.Bass)
			{
				m_Bass = e.Bass;
				ValueChanged = true;
			}

			if (m_Balance != e.Balance)
			{
				m_Balance = e.Balance;
				ValueChanged = true;
			}

			if (m_Source != e.Source)
			{
				m_Source = e.Source;
				ValueChanged = true;
			}
			
			if(ValueChanged && Initialized)
			{
				ZoneChangedEvent?.Invoke(new ZoneChangedEventArgs(AmpID, ZoneID));
			}

			Initialized = true;
		}

		/// <summary>
		/// Creates a link between another zone and this zone
		/// </summary>
		/// <param name="AmpID">The Amp ID of the zone to be linked</param>
		/// <param name="ZoneID">The Zone ID of the zone to be linked</param>
		public void LinkZone(int AmpID, int ZoneID)
		{
			throw new NotImplementedException();

			//
			// Check to make this zone is available for linking
			//
			if (!Enabled)
			{
				throw new ZoneNotEnabledException(@"The primary zone is enabled and cannot be made into a link zone");
			}

			if (LinkStatus == ZoneLinkMode.Secondary)
			{
				throw new ZoneAlreadyLinkedException(@"The primary zone is already a secondary zone for another link.");
			}

			//
			// Check to make sure the secondary zone is available for linking

			//
			// Go ahead and link the zones:
			// Set the primary zone LinkStatus to Primary
			// Set the secondary zone LinkStatus to Secondary
			// Add the secondary zone ID to the primary LinkedZone list
			//
		}
	}
}
