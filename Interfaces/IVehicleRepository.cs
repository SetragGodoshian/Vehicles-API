using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vehicles_API.Models;
using Vehicles_API.ViewModels;

namespace Vehicles_API.Interfaces
{
    public interface IVehicleRepository
    {
        public Task<List<VehicleViewModel>> ListAllVehiclesAsync();
        public Task<VehicleViewModel?> GetVehicleAsync(int id);
        public Task<VehicleViewModel?> GetVehicleAsync(string regNummer);
        public Task AddVehicleAsync(Vehicle model);
        public void DeleteVehicle(int id);
        public void UpdateVehicle(int id, Vehicle model);
        public Task<bool> SaveAllAsync();

    }
}