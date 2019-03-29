using AmpAPI.Services;
using AmpAPI.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MPRSGxZ.Hardware;
using System.Collections.Generic;

namespace AmpAPI.Controllers
{
    [Route("api/amplifier/{amplifierID:int:range(1,3)}/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
		private AmplifierSolutionSettings Settings;
		private IAmplifierService AmplifierService;

		public ZoneController(IOptions<AmplifierSolutionSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.Value;
			this.AmplifierService = AmplifierService;
		}

		// GET: api/Zone
		[HttpGet]
        public IEnumerable<Zone> Get([FromRoute]int AmplifierID)
        {
            return AmplifierService.Amplifiers[AmplifierID - 1].Zones;
        }

        // GET: api/Zone/5
        [HttpGet("{id:int:range(1,6)}")]
        public Zone Get([FromRoute]int AmplifierID, int id)
        {
			return AmplifierService.Amplifiers[AmplifierID - 1].Zones[id - 1];
		}

        // POST: api/Zone
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Zone/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
