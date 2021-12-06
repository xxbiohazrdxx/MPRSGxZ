namespace MPRSGxZ.Commands
{
	class SetCommand : BaseCommand
	{
		public string CommandString { get; private set; }
		public int ResponseIndex { get; private set; }

		internal SetCommand(string CommandString, int MinValue, int MaxValue, int ResponseIndex) : base(MinValue, MaxValue)
		{
			//
			// Set commands never have any response
			//
			this.ExpectedLines = 0;

			this.CommandString = CommandString;
			this.ResponseIndex = ResponseIndex;
		}

		public static implicit operator string(SetCommand Command)
		{
			return $"<{Command.AmpID}{Command.ZoneID}{Command.CommandString}{Command.Value:D2}";
		}
	}
}