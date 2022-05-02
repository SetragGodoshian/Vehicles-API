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

        public Task AddVehicleAsync(Vehicle model)
        {
            throw new NotImplementedException();
        }

        public void DeleteVehicle(int id)
        {
            var response = _context.Vehicles.Find(id);
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

        public void UpdateVehicle(int id, Vehicle model)
        {
            throw new NotImplementedException();
        }
    }
}