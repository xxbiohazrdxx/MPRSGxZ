using System;

namespace MPRSGxZ.Events
{
	public class ZonePollEventArgs : EventArgs
	{
		private int m_AmpID;
		private int m_ZoneID;
		private bool m_PublicAddress;
		private bool m_Power;
		private bool m_Mute;
		private bool m_DoNotDisturb;
		private int m_Volume;
		private int m_Treble;
		private int m_Bass;
		private int m_Balance;
		private int m_Source;

		public ZonePollEventArgs(int AmpID, int ZoneID, bool PublicAddress, bool Power, bool Mute, bool DoNotDisturb, int Volume, int Treble,
										int Bass, int Balance, int Source)
		{
			m_AmpID = AmpID;
			m_ZoneID = ZoneID;
			m_PublicAddress = PublicAddress;
			m_Power = Power;
			m_Mute = Mute;
			m_DoNotDisturb = DoNotDisturb;
			m_Volume = Volume;
			m_Treble = Treble;
			m_Bass = Bass;
			m_Balance = Balance;
			m_Source = Source;
		}

		public int AmpID
		{
			get
			{
				return m_AmpID;
			}
		}

		public int ZoneID
		{
			get
			{
				return m_ZoneID;
			}
		}

		public bool PublicAddress
		{
			get
			{
				return m_PublicAddress;
			}
		}

		public bool Power
		{
			get
			{
				return m_Power;
			}
		}
		
		public bool Mute
		{
			get
			{
				return m_Mute;
			}
		}

		public bool DoNotDisturb
		{
			get
			{
				return m_DoNotDisturb;
			}
		}

		public int Volume
		{
			get
			{
				return m_Volume;
			}
		}

		public int Treble
		{
			get
			{
				return m_Treble;
			}
		}

		public int Bass
		{
			get
			{
				return m_Bass;
			}
		}

		public int Balance
		{
			get
			{
				return m_Bass;
			}
		}

		public int Source
		{
			get
			{
				return m_Source;
			}
		}
	}
}