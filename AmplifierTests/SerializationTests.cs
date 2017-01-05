using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;
using System.IO;
using System.Runtime.Serialization;

namespace AmplifierTests
{
	[TestClass]
	public class SerializationTests
	{
		[TestMethod]
		public void SerializationTest()
		{
			var TestAmplifierSolution = new AmplifierSolution(true);

			var XMLFile = new FileStream("SerializationTest.xml", FileMode.Create);
			var Serializer = new DataContractSerializer(typeof(AmplifierSolution));
			Serializer.WriteObject(XMLFile, TestAmplifierSolution);

			XMLFile.Close();
		}

		[TestMethod]
		public void DeserializationTest()
		{
			var XMLFile = new FileStream("SerializationTest.xml", FileMode.Open);
			var Deserializer = new DataContractSerializer(typeof(AmplifierSolution));

			var TestAmplifierSolution = (AmplifierSolution)Deserializer.ReadObject(XMLFile);

			XMLFile.Close();
		}

		[TestMethod]
		public void SerializationEventTest()
		{
			var TestAmplifierSolution = new AmplifierSolution(true);

			TestAmplifierSolution.Amplifiers[0].Zones[0].Enabled = true;
			TestAmplifierSolution.Amplifiers[0].Zones[0].Name = "Serialization Test";
			TestAmplifierSolution.Amplifiers[0].Zones[0].VolumeFactor = 0.5m;
		}
	}
}
