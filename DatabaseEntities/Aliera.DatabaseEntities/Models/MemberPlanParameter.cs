using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberPlanParameter
    {
        public long MemberId { get; set; }
        public int NoOfDependents { get; set; }
        public int NoOfTobaccoUser { get; set; }
        public int NonPrimaFamilyIndicator { get; set; }
        public int PrimaFamilyIndicator { get; set; }
        public int MemberAge { get; set; }
        public int MaxAge { get; set; }
        public int MinAge { get; set; }
        public bool IsChild { get; set; }
        public bool IsSpouse { get; set; }
    }
}
