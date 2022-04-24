using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using AmpAPI.Models;

namespace AmpAPI.Hubs
{
	public class AmpHub : Hub
	{
		public async Task SendZoneUpdate(ZoneModel Zone) => await Clients.All.SendAsync("ReceiveZoneUpdate", Zone);
	}
}