using System;

namespace MPRSGxZ.Exceptions
{
	public class ZoneNotLinkedException : Exception
	{
		public ZoneNotLinkedException() : base() { }

		public ZoneNotLinkedException(string message) : base(message) {	}
	}
}