using AmpAPI.Settings;
using Microsoft.Extensions.Options;
using MPRSGxZ;
using MPRSGxZ.Hardware;

namespace AmpAPI.Services
{
	public class AmplifierService : IAmplifierService
	{
		public AmplifierSolution AmplifierMiddleware { get; private set; }

		public Amplifier[] Amplifiers
		{
			get
			{
				return AmplifierMiddleware.Amplifiers;
			}
		}
		
		public Source[] Sources
		{
			get
			{
				return AmplifierMiddleware.Sources;
			}
		}
		private AmplifierSolutionSettings Settings;

		public AmplifierService(IOptions<AmplifierSolutionSettings> Settings)
		{
			this.Settings = Settings.Value;
			AmplifierMiddleware = new AmplifierSolution(this.Settings.COMPort, this.Settings.PollingFrequency, this.Settings.AmplifierCount);
			AmplifierMiddleware.Open();
		}
	}
}