namespace MPRSGxZ.Hardware
{
	public class Source
	{
		public int ID { get; internal set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }

		internal Source() { }

		internal Source(int ID)
		{
			this.ID = ID;
			Name = $"Source {ID}";
			Enabled = true;
		}
	}
}