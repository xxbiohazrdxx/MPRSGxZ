using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;
using System.Net;

namespace AmplifierTests
{
	[TestClass]
	public class AmpCommunicationTests
	{
		[TestMethod]
		public void AmpZoneControlTest()
		{
			//var TestAmplifierSolution = new AmplifierStack(@"COM3");
			var SerialServer = new IPEndPoint(IPAddress.Parse("10.1.4.73"), 23);
			var TestAmplifierSolution = new AmplifierStack(SerialServer);
			TestAmplifierSolution.Open();

			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;

			TestAmplifierSolution.Close();
		}
	}
}