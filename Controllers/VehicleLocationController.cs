using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilesCarRental.Models;
using MilesCarRental.Services;
using System;
using System.Linq;

namespace MilesCarRental.Controllers
{
    public class VehicleLocationController : Controller
    {
        ILocationService locationService;
        IVehicleLocationService vehicleLocationService;
        IVehicleService vehicleService;

        public VehicleLocationController (ILocationService locationService, IVehicleLocationService vehicleLocationService, IVehicleService vehicleService)
        {
            this.locationService = locationService;
            this.vehicleLocationService = vehicleLocationService;   
            this.vehicleService = vehicleService;
        }

        // GET: VehicleLocationController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetVehicles([FromBody] LocationRequest locations)
        {
            List<VehicleEntity> vehicles = new List<VehicleEntity> ();

            try
            {
                //Validates if the locations exists
                LocationEntity locationOrigin = locationService.GetByName(locations.LocationOrigin)
                                                                            .First(p => p.IsOrigin == true);

                LocationEntity locationDestination = locationService.GetByName(locations.LocationDestination)
                                                                                 .First(p => p.IsDestination == true);

                if (locationOrigin != null)
                {
                    if (locationDestination != null)
                    {
                        //Obtains the vehicles associated with the locations
                        IEnumerable<VehicleLocationEntity> vehiclesLocation = vehicleLocationService.GetByLocations(locationOrigin.LocationId, locationDestination.LocationId);

                        //Validate if there are vehicles associated with the locations
                        if(vehiclesLocation.Count() > 0)
                        {
                            foreach(VehicleLocationEntity vehicleLocation in vehiclesLocation)
                            {
                                //Get vehicle information
                                vehicles.Add(vehicleService.GetById(vehicleLocation.VehicleId));
                            }

                            return Ok(vehicles);
                        }
                    }
                    else
                    {
                        //Stores information about the location of destination
                        LocationEntity location = new LocationEntity();
                        location.LocationId = new Guid();
                        location.IsDestination = true;
                        location.LocationName = locationDestination.LocationName;
                        location.Registration = DateTime.Now;

                        locationService.Save(location);
                    }
                }
                else
                {
                    //Stores information about the location of origin
                    LocationEntity location = new LocationEntity();
                    location.LocationId = new Guid();
                    location.IsOrigin = true;
                    location.LocationName = locationOrigin.LocationName;
                    location.Registration = DateTime.Now;

                    locationService.Save(location);

                    if (locationDestination == null)
                    {
                        //Stores information about the location of destination
                        location.LocationId = new Guid();
                        location.IsDestination = true;
                        location.LocationName = locationDestination.LocationName;
                        location.Registration = DateTime.Now;

                        locationService.Save(location);
                    }
                }

                vehicles = GenerateVehicles(locationOrigin.LocationId, locationDestination.LocationId);

                return Ok(vehicles);
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Generate a random number to obtain a random vehicle and assign it to the location
        /// </summary>
        /// <param name="locationOrigin"></param>
        /// <param name="locationDestination"></param>
        /// <returns></returns>
        public List<VehicleEntity> GenerateVehicles(Guid locationOrigin, Guid locationDestination)
        {
            int count = 0;
            List<VehicleEntity> vehicles = new List<VehicleEntity>();

            IEnumerable<VehicleEntity> vehiclesBD = vehicleService.Get();

            //Generates a random number of vehicles that does not exceed the maximum number of vehicles in the database
            var randomNumber = new Random().Next(0, vehiclesBD.Count());

            //
            for(int i = 0; i < randomNumber; i++)
            {
                var randomVehicle = new Random().Next(0, vehiclesBD.Count());
                count = 0;

                //Tour each of the stored vehicles
                foreach (VehicleEntity vehicle in vehiclesBD)
                {
                    count++;

                    if (count == randomVehicle)
                    {
                        vehicles.Add(vehicle);

                        //Stores vehicles associated with the location
                        VehicleLocationEntity location = new VehicleLocationEntity();
                        location.VehicleLocationId = new Guid();
                        location.LocationId = locationOrigin;
                        location.VehicleLocationId = vehicle.VehicleId;
                        location.LocationIdDestination = locationDestination;

                        vehicleLocationService.Save(location);
                    }
                }
            }

            return vehicles;
        }
    }
}
