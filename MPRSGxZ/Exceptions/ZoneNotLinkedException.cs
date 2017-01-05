using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPRSGxZ.Exceptions
{
	public class ZoneNotLinkedException : Exception
	{
		public ZoneNotLinkedException() : base()
		{
		}

		public ZoneNotLinkedException(string message) : base(message)
		{
		}
	}
}
