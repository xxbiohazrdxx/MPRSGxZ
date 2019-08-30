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
    public class AmplifierController : ControllerBase
    {
		private AmplifierStackSettings Settings;
		private IAmplifierService AmplifierService;

		public AmplifierController(IOptions<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.Value;
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
        public Amplifier Get(int id)
        {
			return AmplifierService.Amplifiers[id - 1];
        }

        // POST: api/Amplifier
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Amplifier/5
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
