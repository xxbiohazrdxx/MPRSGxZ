using AmpAPI.Models;
using AmpAPI.Services;
using AmpAPI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MPRSGxZ.Hardware;
using System.Collections.Generic;

namespace AmpAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SourceController : ControllerBase
	{
		private AmplifierStackSettings Settings;
		private IAmplifierService AmplifierService;

		public SourceController(IOptions<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.Value;
			this.AmplifierService = AmplifierService;
		}

		// GET: api/Source
		[HttpGet]
		public IEnumerable<Source> Get()
		{
			return AmplifierService.Sources;
		}

		// GET: api/Source/5
		[HttpGet("{id:int:range(1,6)}")]
		public Source Get(int id)
		{
			return AmplifierService.Sources[id - 1];
		}

		// POST: api/Source
		[NonAction]
		[HttpPost]
		public void Post([FromBody] SourceModel value)
		{
		}

		// PUT: api/Source/5
		[HttpPut("{id}")]
		public IActionResult Put(int id, [FromBody] SourceModel value)
		{
			if (id != value.ID)
			{
				return Conflict("Supplied id does not match");
			}
			
			AmplifierService.Sources[id - 1].Name = value.Name;
			AmplifierService.Sources[id - 1].Enabled = value.Enabled;
			return Ok();
		}

		// DELETE: api/ApiWithActions/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
			AmplifierService.Sources[id - 1].Enabled = false;
		}
	}
}
