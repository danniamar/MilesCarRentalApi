using System.ComponentModel.DataAnnotations;

namespace MilesCarRental.Models;

public class LogEntity
{
    [Key]
    public Guid LogId { get; set; }

    [StringLength(50)]
    public string LocationOrigin { get; set; }

    [StringLength(50)]
    public string LocationDestination { get; set; }
    public DateTime Registration { get; set; }
}
