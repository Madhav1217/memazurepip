using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class PlanPackage
    {
        public int PlanPackageId { get; set; }
        public int PlanId { get; set; }
        public string PackagedPlanIds { get; set; }
        public bool IsMigrated { get; set; }

        public virtual Plan Plan { get; set; }
    }
}
