using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;
using MPRSGxZ.Events;
using System.IO;

namespace AmplifierTests
{
	[TestClass]
	public class EventTests
	{
		private bool _ZoneChangedCalled;

		[TestMethod]
		public void ZoneChangedEventTest()
		{
			_ZoneChangedCalled = false;
			var TestAmplifierSolution = new AmplifierStack(@"COM3");
			TestAmplifierSolution.Open();

			TestAmplifierSolution.ZoneChangedEvent += ZoneChanged;

			System.Threading.Thread.Sleep(5000);

			TestAmplifierSolution.Close();

			Assert.IsTrue(_ZoneChangedCalled);
		}

		private void ZoneChanged(ZoneChangedEventArgs e)
		{
			_ZoneChangedCalled = true;
		}
	}
}
