using WebAmp.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace WebAmp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConnectionController : ControllerBase
	{
		private readonly AmplifierStackSettings Settings;

		public ConnectionController(IOptionsMonitor<AmplifierStackSettings> Settings)
		{
			this.Settings = Settings.CurrentValue;
		}

		[HttpGet]
		public ConnectionSettings Get()
		{
			return Settings.Connection;
		}

		[NonAction]
		[HttpPost]
		public void Post([FromBody] AmplifierStackSettings value)
		{
		}

		[HttpPut]
		public void Put([FromBody] AmplifierStackSettings value)
		{
		}

		[HttpDelete]
		public void Delete()
		{
		}
	}
}
