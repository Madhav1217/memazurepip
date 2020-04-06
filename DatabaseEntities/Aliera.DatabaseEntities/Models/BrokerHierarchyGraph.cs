using System.ComponentModel.DataAnnotations;
using Aliera.Utilities.Enumerations;

namespace Aliera.DatabaseEntities.Models
{
    public class BrokerHierarchyGraph
    {
        [Key]
        public BrokerStatus Status { get; set; }
        public int Count { get; set; }
    }
}
