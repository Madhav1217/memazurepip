using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ProductFee
    {
        public int ProductId { get; set; }
        public decimal? Fee { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int BenefitLevel { get; set; }
        public long Id { get; set; }
        public long BrokerId { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Product Product { get; set; }
    }
}
