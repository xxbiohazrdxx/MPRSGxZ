using AmpWeb.Services;
using AmpWeb.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MPRSGxZ.Hardware;
using System.Collections.Generic;


namespace AmpWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
		private AmplifierStackSettings Settings;
		private IAmplifierService AmplifierService;

		public SettingsController(IOptions<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
		{
			this.Settings = Settings.Value;
			this.AmplifierService = AmplifierService;
		}

		// GET: api/Settings
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
			return Ok(Settings);
		}

        // POST: api/Settings
        [HttpPost]
		[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
        public IActionResult Post([FromBody] string value)
        {
			return StatusCode(StatusCodes.Status405MethodNotAllowed);
        }

        // PUT: api/Settings/5
        [HttpPut]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public void Put([FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status405MethodNotAllowed)]
		public IActionResult Delete(int id)
        {
			return StatusCode(StatusCodes.Status405MethodNotAllowed);
		}
    }
}
