using System;
using MPRSGxZ;
using MPRSGxZ.Events;

namespace AmpMonitor
{
	class Program
	{
		private static AmplifierSolution AmpInterface;

		static void Main(string[] args)
		{
			AmpInterface = new AmplifierSolution(@"COM3");
			AmpInterface.ZoneChanged += ZoneChanged;
			AmpInterface.Open();

			Console.CursorVisible = false;
			Console.SetWindowSize(53, 34);

			System.Threading.Thread.Sleep(2000);

			ZoneChanged(null);

			Console.BackgroundColor = ConsoleColor.Red;
			Console.Write(@"Press any key to exit...");
			Console.ReadKey();
		}

		public static void ZoneChanged(ZoneChangedEventArgs e)
		{
			Console.SetCursorPosition(0, 0);

			foreach(var CurrentAmp in AmpInterface.Amplifiers)
			{	
				Console.BackgroundColor = ConsoleColor.DarkGreen;
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine($"==AMP {CurrentAmp.ID}=============================================");

				Console.BackgroundColor = ConsoleColor.DarkRed;
				Console.ForegroundColor = ConsoleColor.White;
				Console.WriteLine(string.Format(@"--Zone---|--{0}1--|--{0}2--|--{0}3--|--{0}4--|--{0}5--|--{0}6--|", CurrentAmp.ID));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Power    |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Power ? 1 : 0,
													CurrentAmp.Zones[1].Power ? 1 : 0,
													CurrentAmp.Zones[2].Power ? 1 : 0,
													CurrentAmp.Zones[3].Power ? 1 : 0,
													CurrentAmp.Zones[4].Power ? 1 : 0,
													CurrentAmp.Zones[5].Power ? 1 : 0));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Mute     |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Mute ? 1 : 0,
													CurrentAmp.Zones[1].Mute ? 1 : 0,
													CurrentAmp.Zones[2].Mute ? 1 : 0,
													CurrentAmp.Zones[3].Mute ? 1 : 0,
													CurrentAmp.Zones[4].Mute ? 1 : 0,
													CurrentAmp.Zones[5].Mute ? 1 : 0));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"PA       |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].PublicAddress ? 1 : 0,
													CurrentAmp.Zones[1].PublicAddress ? 1 : 0,
													CurrentAmp.Zones[2].PublicAddress ? 1 : 0,
													CurrentAmp.Zones[3].PublicAddress ? 1 : 0,
													CurrentAmp.Zones[4].PublicAddress ? 1 : 0,
													CurrentAmp.Zones[5].PublicAddress ? 1 : 0));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"DND      |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].DoNotDisturb ? 1 : 0,
													CurrentAmp.Zones[1].DoNotDisturb ? 1 : 0,
													CurrentAmp.Zones[2].DoNotDisturb ? 1 : 0,
													CurrentAmp.Zones[3].DoNotDisturb ? 1 : 0,
													CurrentAmp.Zones[4].DoNotDisturb ? 1 : 0,
													CurrentAmp.Zones[5].DoNotDisturb ? 1 : 0));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Volume   |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Volume,
													CurrentAmp.Zones[1].Volume,
													CurrentAmp.Zones[2].Volume,
													CurrentAmp.Zones[3].Volume,
													CurrentAmp.Zones[4].Volume,
													CurrentAmp.Zones[5].Volume));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Treble   |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Treble,
													CurrentAmp.Zones[1].Treble,
													CurrentAmp.Zones[2].Treble,
													CurrentAmp.Zones[3].Treble,
													CurrentAmp.Zones[4].Treble,
													CurrentAmp.Zones[5].Treble));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Bass     |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Bass,
													CurrentAmp.Zones[1].Bass,
													CurrentAmp.Zones[2].Bass,
													CurrentAmp.Zones[3].Bass,
													CurrentAmp.Zones[4].Bass,
													CurrentAmp.Zones[5].Bass));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Balance  |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Balance,
													CurrentAmp.Zones[1].Balance,
													CurrentAmp.Zones[2].Balance,
													CurrentAmp.Zones[3].Balance,
													CurrentAmp.Zones[4].Balance,
													CurrentAmp.Zones[5].Balance));

				Console.BackgroundColor = ConsoleColor.DarkBlue;
				Console.Write(@"Source   |");
				Console.BackgroundColor = ConsoleColor.DarkMagenta;
				Console.WriteLine(string.Format(@"  {0:D2}  |  {1:D2}  |  {2:D2}  |  {3:D2}  |  {4:D2}  |  {5:D2}  |",
													CurrentAmp.Zones[0].Source,
													CurrentAmp.Zones[1].Source,
													CurrentAmp.Zones[2].Source,
													CurrentAmp.Zones[3].Source,
													CurrentAmp.Zones[4].Source,
													CurrentAmp.Zones[5].Source));
			}
		}
	}
}