using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;
using MPRSGxZ.Events;
using System.IO;
using System.Runtime.Serialization;

namespace AmplifierTests
{
	[TestClass]
	public class EventTests
	{
		private bool m_ZoneChangedCalled;
		[TestMethod]
		public void ZoneChangedEventTest()
		{
			m_ZoneChangedCalled = false;

			var XMLFile = new FileStream("SerializationTest.xml", FileMode.Open);
			var Deserializer = new DataContractSerializer(typeof(AmplifierSolution));

			var TestAmplifierSolution = (AmplifierSolution)Deserializer.ReadObject(XMLFile);

			XMLFile.Close();

			TestAmplifierSolution.Port.Open();

			TestAmplifierSolution.ZoneChanged += ZoneChanged;

			System.Threading.Thread.Sleep(5000);

			TestAmplifierSolution.Port.Close();

			Assert.IsTrue(m_ZoneChangedCalled);
		}

		private void ZoneChanged(ZoneChangedEventArgs e)
		{
			m_ZoneChangedCalled = true;
		}
	}
}
