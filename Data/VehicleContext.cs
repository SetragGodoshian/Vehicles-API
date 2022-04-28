using Microsoft.EntityFrameworkCore;
using Vehicles_API.Models;

namespace Vehicles_API.Data
{
    public class VehicleContext : DbContext // Steg 1. ärv ifrån EntityFrameworkCore
    {
        public DbSet<Vehicle> Vehicles => Set<Vehicle>(); // Steg 2. Mappa minnesrepresentationen av vårt fordon till databas.
        public VehicleContext(DbContextOptions options) : base(options) { } // Steg 3. Skapa konstruktor för att ta hand om anslutnings konfigurationen.
    }
}