using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aliera.DatabaseEntities.Models
{
    public class BrokerIdTree
    {
        [Key]
        public long BrokerId { get; set; }
    }
}
