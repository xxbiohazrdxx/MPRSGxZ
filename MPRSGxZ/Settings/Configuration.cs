using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPRSGxZ.Exceptions;

namespace MPRSGxZ
{
	public static class Configuration
	{
		/// <summary>
		/// Gets or sets the number of amplifiers in the solution
		/// </summary>
		public static int AmplifierCount
		{
			get
			{
				return Settings.Configuration.Default.AmplifierCount;
			}
			set
			{
				if (value < 1 || value > 3)
				{
					throw new AmplifierCountOutOfRangeException();
				}

				Settings.Configuration.Default.AmplifierCount = value;
				Settings.Configuration.Default.Save();
			}
		}

		/// <summary>
		/// Gets or sets the ComPort to be used to connect to the amplifier
		/// </summary>
		public static string ComPort
		{
			get
			{
				return Settings.Configuration.Default.ComPort;
			}
			set
			{
				Settings.Configuration.Default.ComPort = value;
				Settings.Configuration.Default.Save();
			}
		}

		/// <summary>
		/// Gets or sets the period (in ms) between refreshes of the amplifier state
		/// </summary>
		public static int PollFrequency
		{
			get
			{
				return Settings.Configuration.Default.PollFrequency;
			}
			set
			{
				Settings.Configuration.Default.PollFrequency = value;
				Settings.Configuration.Default.Save();
			}
		}
	}
}
