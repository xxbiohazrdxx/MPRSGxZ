namespace AmpAPI.Models
{
	public class ZoneModel
	{
		public int AmpID { get; private set; }
		public int ZoneID { get; private set; }
		public bool Power { get; private set; }
		public bool Mute { get; private set; }
		public bool PublicAddress { get; private set; }
		public bool DoNotDisturb { get; private set; }
		public int Volume { get; private set; }
		public int Treble { get; private set; }
		public int Bass { get; private set; }
		public int Balance { get; private set; }
		public int Source { get; private set; }
		public string Name { get; private set; }
		public bool Enabled { get; private set; }
		public decimal VolumeFactor { get; private set; }
		public int AdjustedVolume { get; private set; }

		public ZoneModel(int AmpID, int ZoneID, bool Power, bool Mute, bool PublicAddress, bool DoNotDisturb, int Volume, 
							int Treble, int Bass, int Balance, int Source, string Name, bool Enabled, decimal VolumeFactor, int AdjustedVolume)
		{
			this.AmpID = AmpID;
			this.ZoneID = ZoneID;
			this.Power = Power;
			this.Mute = Mute;
			this.PublicAddress = PublicAddress;
			this.DoNotDisturb = DoNotDisturb;
			this.Volume = Volume;
			this.Treble = Treble;
			this.Bass = Bass;
			this.Balance = Balance;
			this.Source = Source;
			this.Name = Name;
			this.Enabled = Enabled;
			this.VolumeFactor = VolumeFactor;
			this.AdjustedVolume = AdjustedVolume;
		}
	}
}
