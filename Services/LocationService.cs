using DataAccess;
using Entities;

namespace MilesCarRental.Services
{
    public class LocationService : ILocationService
    {
        CarRentalContext context;

        public LocationService(CarRentalContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<LocationEntity> Get()
        {
            return context.Locations;
        }

        public async Task Save(LocationEntity location)
        {
            context.Add(location);
            await context.SaveChangesAsync();
        }

        public async Task Update(Guid id, LocationEntity locationUpdate)
        {
            var location = context.Locations.Find(id);

            if (location != null)
            {
                location.LocationName = locationUpdate.LocationName;
                location.IsOrigin = locationUpdate.IsOrigin;
                location.IsDestination = locationUpdate.IsDestination;
                location.Registration = DateTime.Now; 

                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(Guid id)
        {
            var currentLocation = context.Locations.Find(id);

            if (currentLocation != null)
            {
                context.Remove(currentLocation);
                await context.SaveChangesAsync();
            }
        }
    }
    public interface ILocationService
    {
        IEnumerable<LocationEntity> Get();
        Task Save(LocationEntity location);

        Task Update(Guid id, LocationEntity location);

        Task Delete(Guid id);
    }
}

