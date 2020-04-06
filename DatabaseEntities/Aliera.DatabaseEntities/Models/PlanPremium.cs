using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class PlanPremium
    {
        public int PlanId { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public decimal? Msra { get; set; }
        public int FamilyIndicator { get; set; }
        public decimal Premium { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
