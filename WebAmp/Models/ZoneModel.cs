namespace WebAmp.Models
{
	public class ZoneModel
	{
		public int AmpID { get; set; }
		public int ZoneID { get; set; }
		public bool Power { get; set; }
		public bool Mute { get; set; }
		public bool PublicAddress { get; set; }
		public bool DoNotDisturb { get; set; }
		public int Volume { get; set; }
		public int Treble { get; set; }
		public int Bass { get; set; }
		public int Balance { get; set; }
		public int Source { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }
		public decimal VolumeFactor { get; set; }
		public int AdjustedVolume { get; set; }

		internal ZoneModel() { }

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
