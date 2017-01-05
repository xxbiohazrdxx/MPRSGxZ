using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MPRSGxZ;

namespace AmplifierTests
{
	[TestClass]
	public class SettingsTests
	{
		[TestMethod]
		public void AmplifierSettings()
		{
			AmplifierSolution TestingSolution = new AmplifierSolution();
			
			for (int i = 0; i < 3; i++)
			{
				Assert.IsTrue(TestingSolution.Amplifiers[i].ID == i);

				TestingSolution.Amplifiers[i].Name = "Test Name";
				Assert.IsTrue(TestingSolution.Amplifiers[i].Name == "Test Name");
			}
		}

		[TestMethod]
		public void LinkedZoneSettings()
		{
			AmplifierSolution TestingSolution = new AmplifierSolution();

			//var test = TestingSolution.Amplifiers[0].Zones[0].LinkedZones;
		}
	}
}
