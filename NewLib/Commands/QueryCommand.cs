namespace MPRSGxZ.Commands
{
	class QueryCommand : BaseCommand
	{
		internal QueryCommand(int MinValue, int MaxValue) : base(MinValue, MaxValue)
		{
		}

		public static implicit operator string(QueryCommand Command)
		{
			return $"?{Command.AmpID}{Command.ZoneID}";
		}
	}
}