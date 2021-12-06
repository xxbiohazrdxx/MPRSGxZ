namespace MPRSGxZ.Hardware
{
	public class Source
	{
		public int ID { get; internal set; }
		public string Name;
		public bool Enabled;

		internal Source(int ID)
		{
			this.ID = ID;
			Name = $"Source {ID}";
			Enabled = true;
		}
	}
}