using AmpWeb.Settings;
using Microsoft.Extensions.Options;
using MPRSGxZ;
using MPRSGxZ.Hardware;
using System;

namespace AmpWeb.Services
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
		private AmplifierStackSettings _Settings;

		public AmplifierService(IOptions<AmplifierStackSettings> Settings)
		{
			_Settings = Settings.Value;

			if (_Settings.PortType == ConnectionType.Virtual)
			{
				AmplifierMiddleware = new AmplifierStack(_Settings.PollingFrequency, _Settings.AmplifierCount);
			}
			else if (_Settings.PortType == ConnectionType.Serial)
			{
				AmplifierMiddleware = new AmplifierStack(_Settings.PortAddress, _Settings.PollingFrequency, _Settings.AmplifierCount);
			}
			else if (_Settings.PortType == ConnectionType.IP)
			{
				throw new NotImplementedException();
			}
			else
			{
				throw new InvalidOperationException();
			}

			for (int i = 0; i < 6; i++)
			{
				AmplifierMiddleware.Sources[i].Enabled = _Settings.Sources[i].Enabled;
				AmplifierMiddleware.Sources[i].Name = _Settings.Sources[i].Name;
			}
			
			for (int i = 0; i < _Settings.AmplifierCount; i++)
			{
				for (int j = 0; j < 6; j++)
				{
					AmplifierMiddleware.Amplifiers[i].Zones[j].Enabled = _Settings.Amplifiers[i].Zones[j].Enabled;
					AmplifierMiddleware.Amplifiers[i].Zones[j].Name = _Settings.Amplifiers[i].Zones[j].Name;
				}
			}

			AmplifierMiddleware.Open();
		}
	}
}