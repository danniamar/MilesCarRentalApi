﻿using DataAccess;
using MilesCarRental.Models;

namespace MilesCarRental.Services;

public class VehicleService : IVehicleService
{
    CarRentalContext context;

    public VehicleService(CarRentalContext dbcontext)
    {
        context = dbcontext;
    }

    public IEnumerable<VehicleEntity> Get()
    {
        return context.Vehicles;
    }

    public VehicleEntity GetById(Guid vehicleId)
    {
        return context.Vehicles.Where(v => v.VehicleId.Equals(vehicleId)).FirstOrDefault();
    }

    public void Save(VehicleEntity vehicle)
    {
        context.Add(vehicle);
        context.SaveChanges();
    }

    public async Task Update(Guid id, VehicleEntity vehicleUpdate)
    {
        var vehicle = context.Vehicles.Find(id);

        if (vehicle != null)
        {
            vehicle.VehicleName = vehicleUpdate.VehicleName;
            vehicle.VehicleCapacity = vehicleUpdate.VehicleCapacity;
            vehicle.Transmission = vehicleUpdate.Transmission;
            vehicle.QuantityLargeSuitcases = vehicleUpdate.QuantityLargeSuitcases;
            vehicle.QuantitySmallSuitcases = vehicleUpdate.QuantitySmallSuitcases;
            vehicle.IsAvailable = vehicleUpdate.IsAvailable;

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

public interface IVehicleService
{
    IEnumerable<VehicleEntity> Get();
    VehicleEntity GetById(Guid vehicleId);
    void Save(VehicleEntity vehicle);

    Task Update(Guid id, VehicleEntity vehicle);

    Task Delete(Guid id);
}
