namespace MPRSGxZ.Commands
{
	internal class BaseCommand
	{
		public int MinValue { get; private set; }
		public int MaxValue { get; private set; }
		public int AmpID { get; internal set; }
		public int ZoneID { get; internal set; }
		public int Value { get; internal set; }
		public int ExpectedLines { get; internal set; }

		internal BaseCommand(int MinValue, int MaxValue)
		{
			this.MinValue = MinValue;
			this.MaxValue = MaxValue;
		}

		// Queries
		public static BaseCommand Query { get; }			= new QueryCommand(0, 6);

		// Sets
		public static BaseCommand Power { get; }			= new SetCommand(@"PR", 0, 1, 4);
		public static BaseCommand Mute { get; }				= new SetCommand(@"MU", 0, 1, 6);
		public static BaseCommand Volume { get; }			= new SetCommand(@"VO", 0, 38, 10);
		public static BaseCommand Treble { get; }			= new SetCommand(@"TR", 0, 14, 12);
		public static BaseCommand Bass { get; }				= new SetCommand(@"BS", 0, 14, 14);
		public static BaseCommand Balance { get; }			= new SetCommand(@"BL", 0, 14, 16);
		public static BaseCommand Source { get; }			= new SetCommand(@"CH", 1, 6, 18);
		public static BaseCommand PublicAddress { get; }	= new SetCommand(@"PA", 0, 1, 2);
		public static BaseCommand DoNotDisturb { get; }		= new SetCommand(@"DT", 0, 1, 8);
	}
}