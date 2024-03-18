using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MilesCarRental.Models;
using MilesCarRental.Services;
using MilesCarRental.Tools;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace MilesCarRental.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleLocationController : Controller
    {
        ILocationService locationService;
        IVehicleLocationService vehicleLocationService;
        IVehicleService vehicleService;
        ILogService logService;

        private readonly ILogger<VehicleLocationController> _logger;

        public VehicleLocationController (ILocationService locationService, IVehicleLocationService vehicleLocationService, 
            IVehicleService vehicleService, ILogService logService, ILogger<VehicleLocationController> logger)
        {
            this.locationService = locationService;
            this.vehicleLocationService = vehicleLocationService;   
            this.vehicleService = vehicleService;
            this.logService = logService;
            _logger = logger;
        }      


        [HttpPost(Name = "GetVehicles")]
        public ActionResult GetVehicles([FromBody] LocationRequest locations)
        {
            List<VehicleEntity> vehicles = new List<VehicleEntity> ();
            LocationEntity locationOrigin = null;
            LocationEntity locationDestination = null;

            try
            {
                if (locations != null && !string.IsNullOrEmpty(locations.LocationOrigin)  && !string.IsNullOrEmpty(locations.LocationDestination))
                {
                    //Stores a log of the requests made in the method
                    LogEntity log = new LogEntity();
                    log.LogId = new Guid();
                    log.LocationOrigin = locations.LocationOrigin;
                    log.LocationDestination = locations.LocationDestination;
                    log.Registration = DateTime.Now;

                    logService.Save(log);

                    _logger.LogDebug("Consultando las localidades");

                    //Validates if the locations exists
                    IEnumerable<LocationEntity> locationOriginList = locationService.GetByName(locations.LocationOrigin);
                    IEnumerable<LocationEntity> locationDestinationList = locationService.GetByName(locations.LocationDestination);

                    if(locationOriginList != null && locationOriginList.Count() > 0)
                    {
                        locationOrigin = locationOriginList.First(p => p.IsOrigin == true);
                    }

                    if (locationOriginList != null && locationOriginList.Count() > 0)
                    {
                        locationDestination = locationDestinationList.First(p => p.IsDestination == true);
                    }

                    if (locationOrigin != null)
                    {
                        if (locationDestination != null)
                        {
                            _logger.LogDebug("Consultando la lista de vehiculos");
                            //Obtains the vehicles associated with the locations
                            IEnumerable<VehicleLocationEntity> vehiclesLocation = vehicleLocationService.GetByLocations(locationOrigin.LocationId, locationDestination.LocationId);

                            //Validate if there are vehicles associated with the locations
                            if (vehiclesLocation.Count() > 0)
                            {
                                foreach (VehicleLocationEntity vehicleLocation in vehiclesLocation)
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
                            locationDestination = new LocationEntity();
                            locationDestination.LocationId = new Guid();
                            locationDestination.IsDestination = true;
                            locationDestination.LocationName = locations.LocationDestination;
                            locationDestination.Registration = DateTime.Now;

                            locationService.Save(locationDestination);
                        }
                    }
                    else
                    {
                        //Stores information about the location of origin
                        locationOrigin = new LocationEntity();
                        locationOrigin.LocationId = new Guid();
                        locationOrigin.IsOrigin = true;
                        locationOrigin.LocationName = locations.LocationOrigin;
                        locationOrigin.Registration = DateTime.Now;

                        locationService.Save(locationOrigin);

                        if (locationDestination == null)
                        {
                            //Stores information about the location of destination
                            locationDestination = new LocationEntity();
                            locationDestination.LocationId = new Guid();
                            locationDestination.IsDestination = true;
                            locationDestination.LocationName = locations.LocationDestination;
                            locationDestination.Registration = DateTime.Now;

                            locationService.Save(locationDestination);
                        }
                    }

                    List<VehicleEntity> vehiclesBD = vehicleService.Get().ToList();
                                      
                    vehicles = new Utils().GenerateVehicles(vehiclesBD);

                    foreach (VehicleEntity vehicle in vehicles)
                    {
                        //Stores vehicles associated with the location
                        VehicleLocationEntity location = new VehicleLocationEntity();
                        location.VehicleLocationId = new Guid();
                        location.LocationId = locationOrigin.LocationId;
                        location.VehicleLocationId = vehicle.VehicleId;
                        location.LocationIdDestination = locationDestination.LocationId;

                        vehicleLocationService.Save(location);
                    }
                    

                    _logger.LogDebug("Retornando la lista de vehiculos");

                }

                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
