using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPRSGxZ;
using System.IO;
using System.Runtime.Serialization;

namespace AmpMonitor
{
	class Program
	{
		static void Main(string[] args)
		{
			var XMLFile = new FileStream("Settings.xml", FileMode.Open);
			var Deserializer = new DataContractSerializer(typeof(AmplifierSolution));

			var Amplifier = (AmplifierSolution)Deserializer.ReadObject(XMLFile);

			XMLFile.Close();

			Amplifier.Port.Open();

			Console.CursorVisible = false;

			while (true)
			{
				Console.SetWindowSize(91, 34);

				for(int Amp = 0; Amp < 3; Amp++)
				{
					Console.WriteLine(string.Format(@"          ==AMP {0}=========================================================================", Amp + 1));
					Console.WriteLine(              @"          --Zone 1--------Zone 2--------Zone 3--------Zone 4--------Zone 5--------Zone 6--");

					Console.WriteLine(string.Format(@"Power    |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[0].Power), 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[1].Power), 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[2].Power), 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[3].Power), 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[4].Power), 
														Convert.ToInt32(Amplifier.Amplifiers[Amp].Zones[5].Power)));

					Console.WriteLine(string.Format(@"Mute     |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"PA       |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"DND      |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"Volume   |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"Treble   |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"Bass     |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"Balance  |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
					Console.WriteLine(string.Format(@"Source   |    {0:D2}     |    {1:D2}       |    {2:D2}       |    {3:D2}       |    {4:D2}      |    {5:D2}     |", 0, 0, 0, 0, 0, 0));
				}

				System.Threading.Thread.Sleep(100);
				Console.SetCursorPosition(0, 0);
			}
		}
	}
}
