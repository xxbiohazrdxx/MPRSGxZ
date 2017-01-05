using MPRSGxZ.Exceptions;
using System.Runtime.Serialization;
using System;
using MPRSGxZ.Events;

namespace MPRSGxZ
{
	[DataContract]
	public class Amplifier
	{
		private int m_ID;
		private string m_Name;
		private bool m_Enabled;

		private Zone[] m_Zones;
		private int m_ZoneCount;

		private SettingChangedEventHandler SettingChangedEvent;

		//
		// Settings can be amp-wide, not currently in use or implemented
		//
		/*
		private bool m_Power;
		private bool m_Mute;
		private bool m_DoNotDisturb;
		private int m_Volume;
		private int m_Treble;
		private int m_Bass;
		private int m_Balance;
		private int m_Source;
		*/

		/// <summary>
		/// Default blank constructor used for deserialization
		/// </summary>
		public Amplifier()
		{
		}

		/// <summary>
		/// Default constructor for missing configuration
		/// </summary>
		/// <param name="AmpID">The ID of the amplifier</param>
		public Amplifier(int AmpID)
		{
			m_ID = AmpID;
			m_Enabled = false;
			m_Name = string.Format(@"Amp {0}", ID);

			m_ZoneCount = 6;

			m_Zones = new Zone[ZoneCount];

			for(int i = 0; i < ZoneCount; i++)
			{
				m_Zones[i] = new Zone(ID, i + 1);
			}
		}

		internal void AttachSettingChangedEvent(SettingChangedEventHandler SettingChanged)
		{
			SettingChangedEvent = SettingChanged;

			for(int i = 0; i < 6; i++)
			{
				m_Zones[i].AttachSettingChangedEvent(SettingChanged);
			}
		}

		[DataMember]
		public int ID
		{
			get
			{
				return m_ID;
			}
			internal set
			{
				m_ID = value;
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
				if(m_Enabled != value)
				{
					m_Enabled = value;
					SettingChangedEvent?.Invoke();
				}
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
				if(m_Name != value)
				{
					m_Name = value;
					SettingChangedEvent?.Invoke();
				}
			}
		}

		[DataMember]
		public int ZoneCount
		{
			get
			{
				return m_ZoneCount;
			}
			set
			{
				if(m_ZoneCount != value)
				{
					if (value == 4 || value == 6)
					{
						m_ZoneCount = value;
						SettingChangedEvent?.Invoke();
					}
					else
					{
						throw new ArgumentOutOfRangeException();
					}
				}	
			}
		}

		[DataMember]
		public Zone[] Zones
		{
			get
			{
				return m_Zones;
			}
			internal set
			{
				m_Zones = value;
			}
		}
	}
}
