namespace MPRSGxZ.Commands
{
	internal class BaseCommand
	{
		public CommandType Type { get; private set; }
		public string CommandString { get; private set; }
		public int MinValue { get; private set; }
		public int MaxValue { get; private set; }
		public int AmpID { get; internal set; }
		public int ZoneID { get; internal set; }
		public int Value { get; internal set; }
		public int ExpectedLines { get; internal set; }

		public static implicit operator string(BaseCommand Command)
		{
			return Command.CommandString;
		}

		internal BaseCommand(CommandType Type, string CommandString, int MinValue, int MaxValue)
		{
			this.Type = Type;
			this.CommandString = CommandString;
			this.MinValue = MinValue;
			this.MaxValue = MaxValue;
		}

		// Queries
		public static BaseCommand Query { get; } = new BaseCommand(CommandType.Query, @"", 0, 6);

		// Sets
		public static BaseCommand Power { get; } = new BaseCommand(CommandType.Set, @"PR", 0, 1);
		public static BaseCommand Mute { get; } = new BaseCommand(CommandType.Set, @"MU", 0, 1);
		public static BaseCommand Volume { get; } = new BaseCommand(CommandType.Set, @"VO", 0, 38);
		public static BaseCommand Treble { get; } = new BaseCommand(CommandType.Set, @"TR", 0, 14);
		public static BaseCommand Bass { get; } = new BaseCommand(CommandType.Set, @"BS", 0, 14);
		public static BaseCommand Balance { get; } = new BaseCommand(CommandType.Set, @"BL", 0, 14);
		public static BaseCommand Source { get; } = new BaseCommand(CommandType.Set, @"CH", 1, 6);
		public static BaseCommand PublicAddress { get; } = new BaseCommand(CommandType.Set, @"PA", 0, 1);
		public static BaseCommand DoNotDisturb { get; } = new BaseCommand(CommandType.Set, @"DT", 0, 1);
	}
}