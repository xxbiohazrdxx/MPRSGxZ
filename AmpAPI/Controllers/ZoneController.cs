using AmpAPI.Models;
using AmpAPI.Services;
using AmpAPI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MPRSGxZ.Hardware;

namespace AmpAPI.Controllers
{
	[Route("api/amplifier/{amplifierID:int:range(1,3)}/[controller]")]
	[ApiController]
	public class ZoneController : ControllerBase
	{
		private AmplifierStackSettings Settings;
		private IAmplifierService AmplifierService;

		public ZoneController(IOptions<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.Value;
			this.AmplifierService = AmplifierService;
		}

		// GET: api/Zone
		[HttpGet]
		public IActionResult Get([FromRoute]int AmplifierID)
		{
			if (AmplifierID > AmplifierService.Amplifiers.Length)
			{
				return NotFound();
			}

			return Ok(AmplifierService.Amplifiers[AmplifierID - 1].Zones);
		}

		// GET: api/Zone/5
		[HttpGet("{id:int:range(1,6)}")]
		public IActionResult Get([FromRoute]int AmplifierID, int id)
		{
			if (AmplifierID > AmplifierService.Amplifiers.Length)
			{
				return NotFound();
			}

			return Ok(AmplifierService.Amplifiers[AmplifierID - 1].Zones[id - 1]);
		}

		// PUT: api/Zone/5
		[HttpPut("{ZoneID:int:range(1,6)}")]
		public IActionResult Put([FromRoute]int AmplifierID, int ZoneID, ZoneModel PutZone)
		{
			if (AmplifierID > AmplifierService.Amplifiers.Length)
			{
				return NotFound();
			}

			var Zone = AmplifierService.Amplifiers[AmplifierID - 1].Zones[ZoneID - 1];

			Zone.Power = PutZone.Power;
			Zone.Mute = PutZone.Mute;
			Zone.PublicAddress = PutZone.PublicAddress;
			Zone.DoNotDisturb = PutZone.DoNotDisturb;
			Zone.Volume = PutZone.Volume;
			Zone.Treble = PutZone.Treble;
			Zone.Bass = PutZone.Bass;
			Zone.Balance = PutZone.Balance;
			Zone.Source = PutZone.Source;
			Zone.Name = PutZone.Name;
			Zone.Enabled = PutZone.Enabled;
			Zone.VolumeFactor = PutZone.VolumeFactor;

			return Ok();
		}

		[HttpDelete("{ZoneID:int:range(1,6)}")]
		public IActionResult Delete([FromRoute] int AmplifierID, int ZoneID)
		{
			if (AmplifierID > AmplifierService.Amplifiers.Length)
			{
				return NotFound();
			}

			AmplifierService.Amplifiers[AmplifierID - 1].Zones[ZoneID - 1].Enabled = false;
			return Ok();
		}
	}
}
