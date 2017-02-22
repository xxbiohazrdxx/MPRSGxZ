using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;
using System.IO;
using System.Runtime.Serialization;

namespace AmplifierTests
{
	[TestClass]
	public class AmpCommunicationTests
	{
		[TestMethod]
		public void AmpZoneControlTest()
		{
			var XMLFile = new FileStream("SerializationTest.xml", FileMode.Open);
			var Deserializer = new DataContractSerializer(typeof(AmplifierSolution));

			var TestAmplifierSolution = (AmplifierSolution)Deserializer.ReadObject(XMLFile);

			XMLFile.Close();

			TestAmplifierSolution.Port.PortName = "COM6";
			TestAmplifierSolution.Port.Open();

			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = false;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = true;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = true;
			System.Threading.Thread.Sleep(500);
			TestAmplifierSolution.Amplifiers[0].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[0].Zones[5].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[0].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[1].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[2].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[3].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[4].Power = false;
			TestAmplifierSolution.Amplifiers[1].Zones[5].Power = false;

			TestAmplifierSolution.Port.Close();
		}
	}
}
