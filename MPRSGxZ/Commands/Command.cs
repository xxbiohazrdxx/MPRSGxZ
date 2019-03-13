using MPRSGxZ.Exceptions;

namespace MPRSGxZ.Commands
{
	internal class Command : BaseCommand
	{
		internal Command(BaseCommand Command, int AmpID, int ZoneID, int Value) : base(Command.Type, Command.CommandString, Command.MinValue, Command.MaxValue)
		{
			if(AmpID < 1 || AmpID > 3)
			{
				throw new InvalidCommandException("The Amp ID for this command is not valid.");
			}

			if(ZoneID < 0 || ZoneID > 6)
			{
				throw new InvalidCommandException("The Zone ID for this command is not valid.");
			}

			if (Value < Command.MinValue || Value > Command.MaxValue)
			{
				throw new InvalidCommandException("The value for this command is not valid.");
			}

			this.AmpID = AmpID;
			this.ZoneID = ZoneID;
			this.Value = Value;

			if(Command.Type == CommandType.Query)
			{
				if (Value == 0)
				{
					this.ExpectedLines = 6;
				}
				else
				{
					this.ExpectedLines = 1;
				}
			}
			else
			{
				this.ExpectedLines = 0;
			}
		}

		public static implicit operator string(Command Command)
		{
			if(Command.Type == CommandType.Set)
			{
				return $"<{Command.AmpID}{Command.ZoneID}{Command.CommandString}{Command.Value:D2}";
			}
			else
			{
				return $"?{Command.AmpID}{Command.ZoneID}";
			}	
		}
	}
}