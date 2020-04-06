using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class AddOnPlan
    {
        public int AddOnPlanId { get; set; }
        public int PlanId { get; set; }
        public string AddOnPlanIds { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
