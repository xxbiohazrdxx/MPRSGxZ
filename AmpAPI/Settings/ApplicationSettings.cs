namespace AmpAPI.Settings
{
	public class AmplifierStackSettings
	{
		public ConnectionSettings Connection { get; set; }
		public AmplifierSettings[] Amplifiers { get; set; }
		public ZoneSettings[] Zones { get; set; }
		public SourceSettings[] Sources { get; set; }
	}

	public class ConnectionSettings
	{
		public ConnectionType PortType { get; set; }
		public string PortAddress { get; set; }
		public int PollingFrequency { get; set; }
		public int AmplifierCount { get; set; }
	}

	public class AmplifierSettings
	{
		public string Name { get; set; }
		public bool Enabled { get; set; }
	}

	public class ZoneSettings
	{
		public string Name { get; set; }
		public bool Enabled { get; set; }
	}

	public class SourceSettings
	{
		public string Name { get; set; }
		public bool Enabled { get; set; }
	}

	public enum ConnectionType
	{
		Serial,
		IP,
		Virtual
	}
}
