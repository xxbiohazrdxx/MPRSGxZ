using System;

namespace MPRSGxZ.Events
{
	public class ZoneChangedEventArgs : EventArgs
	{
		private int m_AmpID;
		private int m_ZoneID;
		private Command m_Command;
		private int m_Value;

		public ZoneChangedEventArgs(int AmpID, int ZoneID)
		{
			m_AmpID = AmpID;
			m_ZoneID = ZoneID;
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
	}
}