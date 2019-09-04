using System;

namespace MPRSGxZ.Exceptions
{
	public class ZoneAlreadyLinkedException : Exception
	{
		public ZoneAlreadyLinkedException() : base()
		{
		}

		public ZoneAlreadyLinkedException(string message) : base(message)
		{
		}
	}
}