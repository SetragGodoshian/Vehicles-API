using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Controllers
{
    [ApiController]
    [Route("api/v1/vehicles")]
    public class VehiclesController : ControllerBase
    {

        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        public VehiclesController(IVehicleRepository vehicleRepo, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
        }


        // api/v1/vehicles/
        [HttpGet()]
        public async Task<ActionResult<List<VehicleViewModel>>> ListVehicles()
        {
            return Ok(await _vehicleRepo.ListAllVehiclesAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleViewModel>> GetVehicleById(int id)
        {
            try
            {
                var response = await _vehicleRepo.GetVehicleAsync(id);

                if (response is null)
                    return NotFound($"Vi kunde inte hitta någon bil med id: {id}");

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("byregno/{regNo}")]
        public async Task<ActionResult<Vehicle>> GetVehicleByRegNo(string regNo)
        {
            var response = await _vehicleRepo.GetVehicleAsync(regNo);

            if (response is null)
                return NotFound($"Vi kunde inte hitta någon bil med registeringsnummer: {regNo}");

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicle(PostVehicleViewModel model)
        {
            if (await _vehicleRepo.GetVehicleAsync(model.RegNo!.ToLower()) is not null)
            {
                return BadRequest($"Registeringsnummer {model.RegNo} finss redan i systmet");
            }

            await _vehicleRepo.AddVehicleAsync(model);

            if (await _vehicleRepo.SaveAllAsync())
            {
                return StatusCode(201);
            }

            return StatusCode(500, "Det gick inte att spara fordonet");


        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, PostVehicleViewModel model)
        {
            try
            {
                await _vehicleRepo.UpdateVehicle(id, model);
                if (await _vehicleRepo.SaveAllAsync())
                {
                    return NoContent();
                }
                return StatusCode(500, "Ett fel inträffade när fordonet skulle uppdateras");
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            await _vehicleRepo.DeleteVehicle(id);

            if (await _vehicleRepo.SaveAllAsync())
            {
                return NoContent();
            }

            return StatusCode(500, "Hoppsan något gick fel");

        }
    }
}
