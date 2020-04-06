using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerType
    {
        public BrokerType()
        {
            Broker = new HashSet<Broker>();
        }

        public int BrokerTypeId { get; set; }
        public string BrokerTypeName { get; set; }

        public virtual ICollection<Broker> Broker { get; set; }
    }
}
