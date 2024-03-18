using DataAccess;
using Entities;

namespace MilesCarRental.Services
{
    public class LogService : ILogService
    {
        CarRentalContext context;

        public LogService(CarRentalContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<LogEntity> Get()
        {
            return context.Logs;
        }

        public void Save(LogEntity log)
        {
            context.Add(log);
            context.SaveChanges();
        }

        public async Task Update(Guid id, LogEntity logUpdate)
        {
            var log = context.Logs.Find(id);

            if (log != null)
            {
                log.LocationOrigin = logUpdate.LocationOrigin;
                log.LocationDestination = logUpdate.LocationDestination;
                log.Registration = DateTime.Now;

                await context.SaveChangesAsync();
            }
        }
        public async Task Delete(Guid id)
        {
            var currentLog = context.Logs.Find(id);

            if (currentLog != null)
            {
                context.Remove(currentLog);
                await context.SaveChangesAsync();
            }
        }
    }

    public interface ILogService
    {
        IEnumerable<LogEntity> Get();
        void Save(LogEntity log);

        Task Update(Guid id, LogEntity log);

        Task Delete(Guid id);
    }
}
