using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class PlanBenefit
    {
        public int PlanId { get; set; }
        public int BenefitId { get; set; }
        public string Value { get; set; }
    }
}
