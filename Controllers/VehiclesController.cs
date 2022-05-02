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
        private readonly VehicleContext _context;
        private readonly IVehicleRepository _vehicleRepo;
        private readonly IMapper _mapper;
        public VehiclesController(VehicleContext context, IVehicleRepository vehicleRepo, IMapper mapper)
        {
            _mapper = mapper;
            _vehicleRepo = vehicleRepo;
            _context = context;
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
        public async Task<ActionResult<Vehicle>> AddVehicle(PostVehicleViewModel vehicle)
        {
            var vehicleToAdd = _mapper.Map<Vehicle>(vehicle);

            await _context.Vehicles.AddAsync(vehicleToAdd);
            await _context.SaveChangesAsync();
            return StatusCode(201, vehicleToAdd);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateVehicle(int id, Vehicle model)
        {
            var response = await _context.Vehicles.FindAsync(id);

            if (response is null)
                return NotFound($"Vi kunde inte hitta någon bil med id: {id}");

            response.RegNo = model.RegNo;
            response.Make = model.Make;
            response.Model = model.Model;
            response.ModelYear = model.ModelYear;
            response.Mileage = model.Mileage;

            _context.Vehicles.Update(response);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            _vehicleRepo.DeleteVehicle(id);

            if (await _vehicleRepo.SaveAllAsync())
            {
                return NoContent();
            }

            return StatusCode(500, "Hoppsan något gick fel");

        }
    }
}
