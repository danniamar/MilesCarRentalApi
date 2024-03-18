using DataAccess;
using Microsoft.EntityFrameworkCore;
using MilesCarRental.Models;

namespace MilesCarRental.Services;

public class VehicleLocationService : IVehicleLocationService
{
    CarRentalContext context;

    public VehicleLocationService(CarRentalContext dbcontext)
    {
        context = dbcontext;
    }

    public IEnumerable<VehicleLocationEntity> Get()
    {
        return context.VehiclesLocation;
    }

    public IEnumerable<VehicleLocationEntity> GetByLocations(Guid locationOrigin, Guid locationDestination)
    {
        return context.VehiclesLocation
            .Where(v => v.VehicleLocationId.Equals(locationOrigin) && v.LocationIdDestination.Equals(locationDestination));
    }

    public void Save(VehicleLocationEntity vehicleLocation)
    {
        context.Add(vehicleLocation);
        try
        {
            context.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }

    public async Task Update(Guid id, VehicleLocationEntity vehicleLocationUpdate)
    {
        var vehicleLocation = context.VehiclesLocation.Find(id);

        if (vehicleLocation != null)
        {
            vehicleLocation.LocationId = vehicleLocationUpdate.LocationId;
            vehicleLocation.VehicleId = vehicleLocationUpdate.VehicleId;

            await context.SaveChangesAsync();
        }
    }

    public async Task Delete(Guid id)
    {
        var currentVehicle = context.Vehicles.Find(id);

        if (currentVehicle != null)
        {
            context.Remove(currentVehicle);
            await context.SaveChangesAsync();
        }
    }
}
public interface IVehicleLocationService
{
    IEnumerable<VehicleLocationEntity> Get();
    IEnumerable<VehicleLocationEntity> GetByLocations(Guid locationOrigin, Guid locationDestination);
    void Save(VehicleLocationEntity vehicleLocation);

    Task Update(Guid id, VehicleLocationEntity vehicleLocation);

    Task Delete(Guid id);
}
