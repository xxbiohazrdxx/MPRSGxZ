using MPRSGxZ.Exceptions;
using System;
using MPRSGxZ.Events;

namespace MPRSGxZ.Hardware
{
	public class Amplifier
	{
		public int ID { get; private set; }
		public string Name { get; private set; }
		public bool Enabled { get; private set; }

		public Zone[] Zones { get; private set; }
		private int ZoneCount;

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
		/// Default constructor for missing configuration
		/// </summary>
		/// <param name="AmpID">The ID of the amplifier</param>
		internal Amplifier(int AmpID, QueueCommandEvent QueueCommand, ZoneChangedEvent ZoneChanged, int ZoneCount = 6)
		{
			this.ID = AmpID;
			this.Enabled = true;
			this.Name = $"Amp {ID}";

			this.ZoneCount = ZoneCount;

			Zones = new Zone[ZoneCount];

			for(int i = 0; i < ZoneCount; i++)
			{
				Zones[i] = new Zone(ID, i + 1, QueueCommand, ZoneChanged);
			}
		}
	}
}
