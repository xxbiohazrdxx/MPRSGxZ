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
		private AmplifierSolutionSettings Settings;
		private IAmplifierService AmplifierService;

		public SourceController(IOptions<AmplifierSolutionSettings> Settings, IAmplifierService AmplifierService)
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
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Source/5
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
