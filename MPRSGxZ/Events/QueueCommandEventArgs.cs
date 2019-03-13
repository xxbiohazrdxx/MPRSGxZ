using System;
using MPRSGxZ.Commands;

namespace MPRSGxZ.Events
{
	internal class QueueCommandEventArgs : EventArgs
	{
		public Command Command { get; private set; }

		public QueueCommandEventArgs(Command Command)
		{
			this.Command = Command;
		}
	}
}
