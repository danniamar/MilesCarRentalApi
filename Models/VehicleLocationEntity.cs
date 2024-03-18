using Entities;
using System.ComponentModel.DataAnnotations;

namespace MilesCarRental.Models
{
    public class VehicleLocationEntity
    {
        [Key]
        public Guid VehicleLocationId { get; set; }

        [Required]
        public Guid LocationId { get; set; }
        public Guid LocationIdDestination { get; set; }
        public LocationEntity Location { get; set; }

        [Required]
        public Guid VehicleId { get; set; }
        public VehicleEntity Vehicle { get; set; }
    }
}
