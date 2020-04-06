using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberSubscribedPlanHistory
    {
        public long HistoryId { get; set; }
        public long MemberSubscribedPlanId { get; set; }
        public int? Event { get; set; }
        public DateTime? Effective { get; set; }
        public string Comment { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual MemberSubscribedPlan MemberSubscribedPlan { get; set; }
    }
}
