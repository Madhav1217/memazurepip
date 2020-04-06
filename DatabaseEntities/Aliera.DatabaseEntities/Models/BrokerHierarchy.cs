using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerHierarchy
    {
        public int BrokerHierarchyId { get; set; }
        public long BrokerId { get; set; }
        public long ParentBrokerId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Broker ParentBroker { get; set; }
    }
}
