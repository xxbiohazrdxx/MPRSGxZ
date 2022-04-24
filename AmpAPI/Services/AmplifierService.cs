using AmpAPI.Settings;
using Microsoft.Extensions.Options;
using MPRSGxZ;
using MPRSGxZ.Hardware;
using Microsoft.AspNetCore.SignalR;
using AmpAPI.Hubs;

namespace AmpAPI.Services
{
	public class AmplifierService : IAmplifierService
	{
		private readonly IHubContext<AmpHub> Hub;
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

		public void Save()
		{
			//File.WriteAllText(path, JsonSerializer.Serialize(appSettings));
		}

		public AmplifierService(IOptionsMonitor<AmplifierStackSettings> Settings, IHubContext<AmpHub> Hub)
		{
			this.Settings = Settings.CurrentValue;
			this.Hub = Hub;
			//Settings.OnChange

			if (this.Settings.Connection.PortType == ConnectionType.Virtual)
			{
				AmplifierMiddleware = new AmplifierStack(this.Settings.Connection.PollingFrequency, this.Settings.Connection.AmplifierCount);
			}
			else if (this.Settings.Connection.PortType == ConnectionType.Serial)
			{
				AmplifierMiddleware = new AmplifierStack(this.Settings.Connection.PortAddress, this.Settings.Connection.PollingFrequency, this.Settings.Connection.AmplifierCount);
			}
			else
			{
				AmplifierMiddleware = new AmplifierStack(this.Settings.Connection.PortAddress, this.Settings.Connection.PollingFrequency, this.Settings.Connection.AmplifierCount);
			}

			for (int i = 0; i < this.Settings.Connection.AmplifierCount; i++)
			{
				AmplifierMiddleware.Amplifiers[i].Enabled = this.Settings.Amplifiers[i].Enabled;
				AmplifierMiddleware.Amplifiers[i].Name = this.Settings.Amplifiers[i].Name;

				for (int j = 0; j < 6; j++)
				{
					AmplifierMiddleware.Amplifiers[i].Zones[j].Enabled = this.Settings.Zones[(i * 6) + j].Enabled;
					AmplifierMiddleware.Amplifiers[i].Zones[j].Name = this.Settings.Zones[(i * 6) + j].Name;
				}
			}

			for (int i = 0; i < 6; i++)
			{
				AmplifierMiddleware.Sources[i].Name = this.Settings.Sources[i].Name;
				AmplifierMiddleware.Sources[i].Enabled = this.Settings.Sources[i].Enabled;
			}

			AmplifierMiddleware.ZoneChanged += new MPRSGxZ.Events.ZoneChangedEvent(x =>
					Hub.Clients.All.SendAsync("SendZoneUpdate", AmplifierMiddleware.Amplifiers[x.AmpID].Zones[x.ZoneID]));

			AmplifierMiddleware.Open();
		}
	}
}