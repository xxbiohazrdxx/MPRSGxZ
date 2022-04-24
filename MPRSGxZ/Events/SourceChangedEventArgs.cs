using System;

namespace MPRSGxZ.Events
{
	public class SourceChangedEventArgs : EventArgs
	{
		public int ID { get; internal set; }
		public int ZoneID { get; internal set; }

		public SourceChangedEventArgs(int ID)
		{
			this.ID = ID;
		}
	}
}