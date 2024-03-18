using Entities;
using Microsoft.EntityFrameworkCore;
using MilesCarRental.Models;

namespace DataAccess
{
    public class CarRentalContext : DbContext
    {
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<LocationEntity> Locations { get; set; }
        public DbSet<LogEntity> Logs { get; set; }
        public DbSet<VehicleLocationEntity> VehiclesLocation { get; set; }

        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehicleEntity>().HasData(
                new VehicleEntity
                {
                    VehicleId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"), 
                    VehicleName = "Hyundai Accent", 
                    IsAvailable = true, 
                    VehicleCapacity = 5,
                    QuantitySmallSuitcases = 1,
                    QuantityLargeSuitcases = 2,
                    Transmission = "Automática"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("0a2d473b-fe62-4f66-9537-96848c6d5c4b"),
                    VehicleName = "Kia Picanto",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantitySmallSuitcases = 1,
                    QuantityLargeSuitcases = 1,
                    Transmission = "Manual"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("b1820ce5-f0a1-4923-b362-b10ebddc7ad4"),
                    VehicleName = "Suzuki Swift",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantitySmallSuitcases = 1,
                    QuantityLargeSuitcases = 1,
                    Transmission = "Manual"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("74180e03-50cb-4bf3-b645-ef538f769493"),
                    VehicleName = "Renault Duster",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantityLargeSuitcases = 3,
                    Transmission = "Manual"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("7cb616c3-d5d1-4d34-8dd5-d83e51ea3fa8"),
                    VehicleName = "Changan E-Sta",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantitySmallSuitcases = 1,
                    QuantityLargeSuitcases = 1,
                    Transmission = "Automática"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("d70f3c46-0055-4b30-946a-98486ec75993"),
                    VehicleName = "Renault Koleos",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantityLargeSuitcases = 3,
                    Transmission = "Automática"
                },
                new VehicleEntity
                {
                    VehicleId = new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490"),
                    VehicleName = "Hyundai Creta",
                    IsAvailable = true,
                    VehicleCapacity = 5,
                    QuantitySmallSuitcases = 1,
                    QuantityLargeSuitcases = 1,
                    Transmission = "Automática"
                }
                );

            modelBuilder.Entity<LocationEntity>().HasData(
                new LocationEntity
                {
                    LocationId = new Guid("b29f780f-c2fc-41e0-a5e5-dab0090a4f09"),
                    LocationName = "location"
                }
                );

            modelBuilder.Entity<VehicleLocationEntity>().HasData(
                new VehicleLocationEntity
                { 
                    VehicleLocationId = new Guid("ecefad67-95ed-4323-bb9b-96c0a0305027"),
                    LocationId = new Guid("b29f780f-c2fc-41e0-a5e5-dab0090a4f09"),
                    VehicleId = new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490")
                }
                );
        }
    }
}
