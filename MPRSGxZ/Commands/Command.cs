using MPRSGxZ.Exceptions;
using System;

namespace MPRSGxZ.Commands
{
	internal class Command : BaseCommand
	{
		private BaseCommand _BaseCommand;
		public CommandType Type { get; private set; }
		public int ResponseIndex
		{
			get
			{
				if (_BaseCommand is SetCommand)
				{
					var Set = (SetCommand)this._BaseCommand;
					return Set.ResponseIndex;
				}

				throw new InvalidOperationException(@"ResponseIndex is not valid on a QueryCommand");
			}
		}

		internal Command(BaseCommand Command, int AmpID, int ZoneID, int Value) : base(Command.MinValue, Command.MaxValue)
		{
			if (AmpID < 1 || AmpID > 3)
			{
				throw new InvalidCommandException("The Amp ID for this command is not valid.");
			}

			if (ZoneID < 0 || ZoneID > 6)
			{
				throw new InvalidCommandException("The Zone ID for this command is not valid.");
			}

			if (Value < Command.MinValue || Value > Command.MaxValue)
			{
				throw new InvalidCommandException("The value for this command is not valid.");
			}

			_BaseCommand = Command;
			this.AmpID = AmpID;
			this.ZoneID = ZoneID;
			this.Value = Value;

			if (Command is QueryCommand)
			{
				Type = CommandType.Query;

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
				Type = CommandType.Set;
			}
		}

		public static implicit operator string(Command Command)
		{
			if (Command._BaseCommand is QueryCommand Query)
			{
				return (string)Query;
			}
			else
			{
				var Set = (SetCommand)Command._BaseCommand;
				return (string)Set;
			}
		}
	}
}