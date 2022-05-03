using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Vehicles_API.Controllers
{
    [Route("[api/vi/manufacturers]")]
    public class ManufacturerController : Controller
    {
        [HttpGet()]
        public async Task<ActionResult> ListAllManufacturers()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetManufacturerById()
        {
            return Ok();
        }

        [HttpPost()]
        public async Task<ActionResult> AddManufacturer()
        {
            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateManufacturer(int id)
        {
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteManufacturer(int id)
        {
            return NoContent();
        }
    }
}