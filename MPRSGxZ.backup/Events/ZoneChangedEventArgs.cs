using System;

namespace MPRSGxZ.Events
{
	public class ZoneChangedEventArgs : EventArgs
	{
		public int AmpID { get; internal set; }
		public int ZoneID { get; internal set; }

		public ZoneChangedEventArgs(int AmpID, int ZoneID)
		{
			this.AmpID = AmpID;
			this.ZoneID = ZoneID;
		}
	}
}