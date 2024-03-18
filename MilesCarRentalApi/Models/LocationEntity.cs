using System.ComponentModel.DataAnnotations;

namespace MilesCarRental.Models;

public class LocationEntity
{
    [Key]
    public Guid LocationId { get; set; }

    [Required]
    [StringLength(250)]
    public string LocationName { get; set; }
    public bool IsOrigin { get; set; }
    public bool IsDestination { get; set; }
    public DateTime Registration { get; set; }
}
