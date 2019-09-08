using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using AmpWeb.Models;

namespace AmpWeb.Hubs
{
    public class AmpHub : Hub
    {
        public async Task SendZoneUpdate(ZoneModel Zone)
        {
            await Clients.All.SendAsync("ReceiveZoneUpdate", Zone);
        }
    }
}