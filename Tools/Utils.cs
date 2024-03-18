using MilesCarRental.Models;

namespace MilesCarRental.Tools;

public class Utils
{
    /// <summary>
    /// Generate a random number to obtain a random vehicle and assign it to the location
    /// </summary>
    /// <param name="locationOrigin"></param>
    /// <param name="locationDestination"></param>
    /// <returns></returns>
    public List<VehicleEntity> GenerateVehicles(List<VehicleEntity> vehiclesList)
    {
        int count = 0;
        List<VehicleEntity> vehicles = new List<VehicleEntity>();
       
        int totalVehicles = vehiclesList.Count();

        //Generates a random number of vehicles that does not exceed the maximum number of vehicles in the database
        var randomNumber = new Random().Next(0, totalVehicles);

        //
        for (int i = 0; i < randomNumber; i++)
        {
            var randomVehicle = new Random().Next(0, totalVehicles);
            count = 0;

            //Tour each of the stored vehicles
            foreach (VehicleEntity vehicle in vehiclesList)
            {
                count++;

                if (count == randomVehicle)
                {
                    vehicles.Add(vehicle);
                }
            }
        }

        return vehicles;
    }

    public LogEntity CreateLog(string locationOrigin, string locationDestination)
    {
        LogEntity log = new LogEntity();

        log.LogId = new Guid();
        log.LocationOrigin = locationOrigin;
        log.LocationDestination = locationDestination;
        log.Registration = DateTime.Now;

        return log;
    }

    public LocationEntity CreateLocation(string locationName, bool isOrigin)
    {
        LocationEntity location = new LocationEntity();

        location.LocationId = new Guid();
        location.IsOrigin = (isOrigin) ? true : false;
        location.IsDestination = (isOrigin)? false: true;
        location.LocationName = locationName;
        location.Registration = DateTime.Now;

        return location;
    }

    public VehicleLocationEntity CreateVehicleLocation(Guid locationIdOrigin, Guid locationIdDestination, Guid vehicleId)
    {
        VehicleLocationEntity vehicleLocation = new VehicleLocationEntity();

        vehicleLocation.VehicleLocationId = new Guid();
        vehicleLocation.LocationId = locationIdOrigin;
        vehicleLocation.VehicleLocationId = vehicleId;
        vehicleLocation.LocationIdDestination = locationIdDestination;

        return vehicleLocation;
    }
}
