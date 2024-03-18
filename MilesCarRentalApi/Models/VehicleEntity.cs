using System.ComponentModel.DataAnnotations;

namespace MilesCarRental.Models;

public class VehicleEntity
{
    [Key]
    public Guid VehicleId { get; set; }

    [Required]
    [StringLength(100)]
    public string VehicleName { get; set; }

    [Required]
    public int VehicleCapacity { get; set; }
    public int QuantityLargeSuitcases { get; set; }
    public int QuantitySmallSuitcases { get; set; }

    [Required]
    [StringLength (20)]
    public string Transmission { get; set; }

    [Required]
    public bool IsAvailable { get; set; }
}
