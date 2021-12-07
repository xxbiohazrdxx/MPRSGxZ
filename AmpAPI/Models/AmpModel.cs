namespace AmpAPI.Models
{
	public class AmpModel
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public bool Enabled { get; set; }

		public ZoneModel[] Zones { get; set; }

		public AmpModel() { }
	}
}
