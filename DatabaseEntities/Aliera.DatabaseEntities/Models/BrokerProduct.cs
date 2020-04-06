using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerProduct
    {
        public long BrokerId { get; set; }
        public int ProductId { get; set; }
        public decimal? ApplicationFee { get; set; }
        public string NoSaleStates { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Product Product { get; set; }
    }
}
