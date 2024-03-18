using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MilesCarRental.Controllers;
using MilesCarRental.Models;
using MilesCarRental.Services;
using Moq;

namespace UnitTestMilesCarRental;

public class VehicleLocationControllerTest
{
    [Fact]
    public void GetVehiclesTest()
    {
        var mockLocationService = new Mock<ILocationService>();
        var mockVehicleLocationService = new Mock<IVehicleLocationService>();
        var mockVehicleService = new Mock<IVehicleService>();
        var mockLogService = new Mock<ILogService>();
        var mockLogger = new Mock<ILogger<VehicleLocationController>>();


        var controller = new VehicleLocationController(mockLocationService.Object, mockVehicleLocationService.Object,
                                                        mockVehicleService.Object, mockLogService.Object, mockLogger.Object);

       
        mockVehicleService.Setup(service => service.Get()).Returns(GetVehiclesList());

        LocationRequest locations = new LocationRequest();
        locations.LocationOrigin = "usaquen";
        locations.LocationDestination = "suba";
        
        var result = controller.GetVehicles(locations);
        var objectResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<VehicleEntity>>(objectResult.Value);

        Assert.NotNull(result);
        Assert.True(value.Count() > 0);
    }

    [Fact]
    public void GetVehiclesLocalitiesExistWithVehiclesTest()
    {
        var mockLocationService = new Mock<ILocationService>();
        var mockVehicleLocationService = new Mock<IVehicleLocationService>();
        var mockVehicleService = new Mock<IVehicleService>();
        var mockLogService = new Mock<ILogService>();
        var mockLogger = new Mock<ILogger<VehicleLocationController>>();


        var controller = new VehicleLocationController(mockLocationService.Object, mockVehicleLocationService.Object,
                                                        mockVehicleService.Object, mockLogService.Object, mockLogger.Object);

        LocationRequest locations = new LocationRequest();
        locations.LocationOrigin = "usaquen";
        locations.LocationDestination = "suba";

        IEnumerable<LocationEntity> locationOriginList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "usaquen",
            IsOrigin = true
        } };

        IEnumerable<LocationEntity> locationDestinationList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "suba",
            IsDestination = true
        } };

        mockLocationService.Setup(service => service.GetByName(locations.LocationOrigin)).Returns(locationOriginList);
        mockLocationService.Setup(service => service.GetByName(locations.LocationDestination)).Returns(locationDestinationList);

        IEnumerable<VehicleLocationEntity> vehiclesLocation = new List<VehicleLocationEntity>()
        {
            new VehicleLocationEntity
            {
                VehicleLocationId = new Guid("0a2d473b-fe62-4f66-9537-96848c6d5c4b"),
                LocationId= new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
                LocationIdDestination = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
                VehicleId = new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490")
            }
        };

        mockVehicleLocationService.Setup(service => service.GetByLocations(new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"), new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"))).
            Returns(vehiclesLocation);

        VehicleEntity vehicleEntity = new VehicleEntity
        {
            VehicleId = new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490"),
            VehicleName = "Hyundai Creta",
            IsAvailable = true,
            VehicleCapacity = 5,
            QuantitySmallSuitcases = 1,
            QuantityLargeSuitcases = 1,
            Transmission = "Automática"
        };

        mockVehicleService.Setup(service => service.GetById(new Guid("0c4c3455-4330-4e35-b0a7-9ec7e42c6490"))).Returns(vehicleEntity);
        mockVehicleService.Setup(service => service.Get()).Returns(GetVehiclesList());

        var result = controller.GetVehicles(locations);
        var objectResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<VehicleEntity>>(objectResult.Value);

        Assert.NotNull(result);
        Assert.True(value.Count() > 0);
    }

    [Fact]
    public void GetVehiclesLocalitiesExistWithoutVehiclesTest()
    {
        var mockLocationService = new Mock<ILocationService>();
        var mockVehicleLocationService = new Mock<IVehicleLocationService>();
        var mockVehicleService = new Mock<IVehicleService>();
        var mockLogService = new Mock<ILogService>();
        var mockLogger = new Mock<ILogger<VehicleLocationController>>();


        var controller = new VehicleLocationController(mockLocationService.Object, mockVehicleLocationService.Object,
                                                        mockVehicleService.Object, mockLogService.Object, mockLogger.Object);

        LocationRequest locations = new LocationRequest();
        locations.LocationOrigin = "usaquen";
        locations.LocationDestination = "suba";

        IEnumerable<LocationEntity> locationOriginList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "usaquen",
            IsOrigin = true
        } };

        IEnumerable<LocationEntity> locationDestinationList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "suba",
            IsDestination = true
        } };

        mockLocationService.Setup(service => service.GetByName(locations.LocationOrigin)).Returns(locationOriginList);
        mockLocationService.Setup(service => service.GetByName(locations.LocationDestination)).Returns(locationDestinationList);

        mockVehicleService.Setup(service => service.Get()).Returns(GetVehiclesList());

        var result = controller.GetVehicles(locations);
        var objectResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<VehicleEntity>>(objectResult.Value);

        Assert.NotNull(result);
        Assert.True(value.Count() > 0);
    }

    [Fact]
    public void GetVehicleDestinationNotExistTest()
    {
        var mockLocationService = new Mock<ILocationService>();
        var mockVehicleLocationService = new Mock<IVehicleLocationService>();
        var mockVehicleService = new Mock<IVehicleService>();
        var mockLogService = new Mock<ILogService>();
        var mockLogger = new Mock<ILogger<VehicleLocationController>>();


        var controller = new VehicleLocationController(mockLocationService.Object, mockVehicleLocationService.Object,
                                                        mockVehicleService.Object, mockLogService.Object, mockLogger.Object);

        LocationRequest locations = new LocationRequest();
        locations.LocationOrigin = "usaquen";
        locations.LocationDestination = "suba";

        IEnumerable<LocationEntity> locationOriginList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "usaquen",
            IsOrigin = true
        } };

        IEnumerable<LocationEntity> locationDestinationList = new List<LocationEntity>(){
        new LocationEntity
        {
            LocationId = new Guid("2a4f2245-f6ab-4626-bf5f-c16216dd6822"),
            LocationName = "suba",
            IsOrigin = true
        } };

        mockLocationService.Setup(service => service.GetByName(locations.LocationOrigin)).Returns(locationOriginList);
        mockLocationService.Setup(service => service.GetByName(locations.LocationDestination)).Returns(locationDestinationList);

        mockVehicleService.Setup(service => service.Get()).Returns(GetVehiclesList());

        var result = controller.GetVehicles(locations);
        var objectResult = Assert.IsType<OkObjectResult>(result);

        var value = Assert.IsAssignableFrom<IEnumerable<VehicleEntity>>(objectResult.Value);

        Assert.NotNull(result);
        Assert.True(value.Count() > 0);
    }

    public IEnumerable<VehicleEntity> GetVehiclesList()
    {
        IEnumerable<VehicleEntity> vehicles = new List<VehicleEntity>() {
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
        };
        return vehicles;
    }       
}