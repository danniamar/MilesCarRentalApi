using Entities;
using MilesCarRental.Controllers;
using MilesCarRental.Models;
using MilesCarRental.Services;

namespace MilesCarRental.Tools
{
    public class Utils
    {
        /// <summary>
        /// Generate a random number to obtain a random vehicle and assign it to the location
        /// </summary>
        /// <param name="locationOrigin"></param>
        /// <param name="locationDestination"></param>
        /// <returns></returns>
        public List<VehicleEntity> GenerateVehicles(List<VehicleEntity> vehiclesBD)
        {
            int count = 0;
            List<VehicleEntity> vehicles = new List<VehicleEntity>();
           
            int totalVehicles = (vehiclesBD != null) ? vehiclesBD.Count() : 7;

            //Generates a random number of vehicles that does not exceed the maximum number of vehicles in the database
            var randomNumber = new Random().Next(0, totalVehicles);

            //
            for (int i = 0; i < randomNumber; i++)
            {
                var randomVehicle = new Random().Next(0, totalVehicles);
                count = 0;

                //Tour each of the stored vehicles
                foreach (VehicleEntity vehicle in vehiclesBD)
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
    }
}
