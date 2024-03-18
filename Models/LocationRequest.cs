using System.ComponentModel.DataAnnotations;

namespace MilesCarRental.Models;

public class LocationRequest
{
    [StringLength(250)]
    public string LocationOrigin { get; set; }

    [StringLength(250)]
    public string LocationDestination { get; set; }
}
