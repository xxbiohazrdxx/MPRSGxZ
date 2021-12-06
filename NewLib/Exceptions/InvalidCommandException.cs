using System;

namespace MPRSGxZ.Exceptions
{
	class InvalidCommandException : Exception
	{
		public InvalidCommandException(string Message) : base(Message) { }
	}
}