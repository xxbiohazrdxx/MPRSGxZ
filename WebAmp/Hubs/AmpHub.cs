using Microsoft.AspNetCore.SignalR;

namespace WebAmp.Hubs
{
    public class AmpHub : Hub<IAmpHub>
	{
		public async Task SendUpdate() => await Clients.All.SendUpdate();
	}
}