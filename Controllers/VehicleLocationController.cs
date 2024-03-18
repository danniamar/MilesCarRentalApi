using Microsoft.AspNetCore.Mvc;
using MilesCarRental.Models;
using MilesCarRental.Services;
using MilesCarRental.Tools;

namespace MilesCarRental.Controllers;

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
            if ( !string.IsNullOrEmpty(locations?.LocationOrigin)  && !string.IsNullOrEmpty(locations?.LocationDestination))
            {
                //Stores a log of the requests made in the method
                LogEntity log = new Utils().CreateLog(locations.LocationOrigin, locations.LocationDestination);
                logService.Save(log);

                _logger.LogDebug("Consultando las localidades");

                //Validates if the locations exists
                List<LocationEntity> locationOriginList = locationService.GetByName(locations.LocationOrigin).ToList();
                List<LocationEntity> locationDestinationList = locationService.GetByName(locations.LocationDestination).ToList();

                if(locationOriginList != null && locationOriginList.Count() > 0)
                {
                    locationOrigin = locationOriginList?.FirstOrDefault(p => p.IsOrigin.Equals(true));
                }

                if (locationOriginList != null && locationOriginList.Count() > 0)
                {
                    locationDestination = locationDestinationList?.FirstOrDefault(p => p.IsDestination.Equals(true));
                }

                if (locationOrigin != null)
                {
                    if (locationDestination != null)
                    {
                        _logger.LogDebug("Consultando la lista de vehiculos");
                        //Obtains the vehicles associated with the locations
                        IEnumerable<VehicleLocationEntity> vehiclesLocation = vehicleLocationService.GetByLocations(locationOrigin.LocationId, locationDestination.LocationId);

                        if (vehiclesLocation?.Count() > 0)
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
                        locationDestination = new Utils().CreateLocation(locations.LocationDestination, false);

                        locationService.Save(locationDestination);
                    }
                }
                else
                {
                    //Stores information about the location of origin
                    locationOrigin = new Utils().CreateLocation(locations.LocationOrigin, true);

                    locationService.Save(locationOrigin);

                    if (locationDestination == null)
                    {
                        //Stores information about the location of destination
                        locationDestination = new Utils().CreateLocation(locations.LocationDestination, false);

                        locationService.Save(locationDestination);
                    }
                }

                List<VehicleEntity> vehiclesList = vehicleService.Get().ToList();
                                  
                vehicles = new Utils().GenerateVehicles(vehiclesList);

                foreach (VehicleEntity vehicle in vehicles)
                {
                    //Stores vehicles associated with the location
                    VehicleLocationEntity vehicleLocation = new Utils().CreateVehicleLocation(locationOrigin.LocationId, locationDestination.LocationId, vehicle.VehicleId);

                    vehicleLocationService.Save(vehicleLocation);
                }                    

                _logger.LogDebug("Retornando la lista de vehiculos");

            }
            else
            {
                return Ok("Error:Por favor ingresar la localidad de origen y destino.");
            }

            return Ok(vehicles);
        }
        catch (InvalidOperationException ex)
        {
            return Ok($"Error: Al cosultar los vehiculos por localidad de origen y destino. {ex.Message}");
        }
    }
}
