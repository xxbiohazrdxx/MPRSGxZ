using System;

namespace MPRSGxZ.Exceptions
{
	public class ZoneNotEnabledException : Exception
	{
		public ZoneNotEnabledException() : base() {	}

		public ZoneNotEnabledException(string message) : base(message) { }
	}
}