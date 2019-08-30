using AmpAPI.Settings;
using Microsoft.Extensions.Options;
using MPRSGxZ;
using MPRSGxZ.Hardware;
using System;

namespace AmpAPI.Services
{
	public class AmplifierService : IAmplifierService
	{
		public AmplifierStack AmplifierMiddleware { get; private set; }

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
		private AmplifierStackSettings Settings;

		public AmplifierService(IOptions<AmplifierStackSettings> Settings)
		{
			this.Settings = Settings.Value;

			if (this.Settings.PortType == ConnectionType.Virtual)
			{
				AmplifierMiddleware = new AmplifierStack(this.Settings.PollingFrequency, this.Settings.AmplifierCount);
			}
			else if (this.Settings.PortType == ConnectionType.Serial)
			{
				AmplifierMiddleware = new AmplifierStack(this.Settings.PortAddress, this.Settings.PollingFrequency, this.Settings.AmplifierCount);
			}
			else
			{
				throw new NotImplementedException();
			}

			AmplifierMiddleware.Open();
		}
	}
}