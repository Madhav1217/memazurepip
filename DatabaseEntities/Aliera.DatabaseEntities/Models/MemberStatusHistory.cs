using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberStatusHistory
    {
        public long MemberStatusHistoryId { get; set; }
        public long MemberId { get; set; }
        public long MemberSubscribedPlanId { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InActiveDate { get; set; }
        public string ActiveDateComment { get; set; }
        public string InActiveDateComment { get; set; }
        public string OnHoldDateComment { get; set; }
        public DateTime? HoldDate { get; set; }
        public DateTime? CancelDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual MemberSubscribedPlan MemberSubscribedPlan { get; set; }
    }
}
