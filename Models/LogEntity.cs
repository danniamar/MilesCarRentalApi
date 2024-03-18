using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class LogEntity
    {
        [Key]
        public Guid LogId { get; set; }
        [StringLength(50)]
        public bool LocationOrigin { get; set; }
        [StringLength(50)]
        public bool LocationDestination { get; set; }
        public DateTime Registration { get; set; }
    }
}
