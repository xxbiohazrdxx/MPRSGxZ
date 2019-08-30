using AmpAPI.Models;
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
		private AmplifierStackSettings Settings;
		private IAmplifierService AmplifierService;

		public ZoneController(IOptions<AmplifierStackSettings> Settings, IAmplifierService AmplifierService)
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

		// PUT: api/Zone/5
		[HttpPut("{ZoneID:int:range(1,6)}")]
		public void Put([FromRoute]int AmplifierID, int ZoneID, ZoneModel PutZone)
		{
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
		}

		// PUT: api/Zone/5
		//[HttpPut("{ZoneID:int:range(1,6)}/volume/{value:int:range(0,38)}")]
		//public void Put([FromRoute]int AmplifierID, int ZoneID, int value)
		//{
		//}

		// PUT: api/Zone/5
		//[HttpPut("{ZoneID:int:range(1,6)}/volume")]
		//public void Put([FromRoute]int AmplifierID, int ZoneID, [FromBody]int Volume)
		//{
		//}

		// No POST/DELETE as zones are never created/deleted
		// POST: api/Zone
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}
		// DELETE: api/ApiWithActions/5
		//[HttpDelete("{id}")]
		//      public void Delete(int id)
		//      {
		//      }
	}
}
