using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Vehicles_API.Data;
using Vehicles_API.Interfaces;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VehicleContext _context;
        private readonly IMapper _mapper;
        public VehicleRepository(VehicleContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task AddVehicleAsync(PostVehicleViewModel model)
        {
            var vehicleToAdd = _mapper.Map<Vehicle>(model);
            await _context.Vehicles.AddAsync(vehicleToAdd);
        }

        public async Task DeleteVehicle(int id)
        {
            var response = await _context.Vehicles.FindAsync(id);
            if (response is not null)
            {
                _context.Vehicles.Remove(response);
            }
        }

        public async Task<VehicleViewModel?> GetVehicleAsync(int id)
        {
            return await _context.Vehicles.Where(c => c.Id == id)
                .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<VehicleViewModel?> GetVehicleAsync(string regNumber)
        {
            return await _context.Vehicles.Where(c => c.RegNo == regNumber)
            .ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<List<VehicleViewModel>> ListAllVehiclesAsync()
        {
            return await _context.Vehicles.ProjectTo<VehicleViewModel>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task UpdateVehicle(int id, PostVehicleViewModel model)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);

            if (vehicle is null)
            {
                throw new Exception($"Vi kunde inte hitta något fordon med id: {id}");
            }

            vehicle.RegNo = model.RegNo;
            vehicle.Make = model.Make;
            vehicle.Model = model.Model;
            vehicle.ModelYear = model.ModelYear;
            vehicle.Mileage = model.Mileage;

            _context.Vehicles.Update(vehicle);

        }
    }
}