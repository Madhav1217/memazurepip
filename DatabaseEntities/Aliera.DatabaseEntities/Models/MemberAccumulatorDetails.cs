using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberAccumulatorDetails
    {
        public long MemberAccumulatorDetailsId { get; set; }
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public string AccumulatorType { get; set; }
        public decimal AllocatedAmount { get; set; }
        public decimal ConsumedAmount { get; set; }
        public bool IsFamilyAccumulator { get; set; }
        public string NetworkTier { get; set; }
        public string Description { get; set; }
        public string ProvisionText { get; set; }
        public DateTime EffectiveStartDate { get; set; }
        public DateTime EffectiveEndDate { get; set; }
        public long AccumulatorId { get; set; }

        public virtual Accumulator Accumulator { get; set; }
        public virtual Member Member { get; set; }
        public virtual MemberDetail MemberDetail { get; set; }
    }
}
