using Entities;

namespace MilesCarRental.Models
{
    public class VehicleLocationEntity
    {        
        public Guid LocationId { get; set; }
        public LocationEntity Location { get; set; }
        public Guid VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
