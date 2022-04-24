using WebAmp.Models;
using WebAmp.Services;
using WebAmp.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MPRSGxZ.Hardware;

namespace WebAmp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AmplifierController : ControllerBase
	{
		private readonly AmplifierStackSettings Settings;
		private readonly IAmplifierService AmplifierService;

		public AmplifierController(IOptionsMonitor<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.CurrentValue;
			this.AmplifierService = AmplifierService;
		}

		// GET: api/Amplifier
		[HttpGet]
		public IEnumerable<Amplifier> Get()
		{
			return AmplifierService.Amplifiers;
		}

		// GET: api/Amplifier/5
		[HttpGet("{id:int:range(1,3)}")]
		public IActionResult Get(int id)
		{
			if (id > AmplifierService.Amplifiers.Length)
			{
				return NotFound();
			}

			return Ok(AmplifierService.Amplifiers[id - 1]);
		}

		// POST: api/Amplifier
		[NonAction]
		[HttpPost]
		public void Post([FromBody] AmpModel value)
		{
		}

		// PUT: api/Amplifier/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] AmpModel value)
		{
			return Conflict($"The object contains modifications to properties that are read only.");
			AmplifierService.Amplifiers[id - 1].Enabled = value.Enabled;
			AmplifierService.Amplifiers[id - 1].Name = value.Name;
			return Ok();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			AmplifierService.Amplifiers[id - 1].Enabled = false;
		}
	}
}
