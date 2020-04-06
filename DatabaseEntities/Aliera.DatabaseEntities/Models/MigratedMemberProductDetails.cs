using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MigratedMemberProductDetails
    {
        public string MaximumTermSharingCv { get; set; }
        public string CareVanMsra { get; set; }
        public string CarePlusMsra { get; set; }
        public string MaximumTermSharingCpnt { get; set; }
        public string MaximumTermSharingEcp { get; set; }
        public string EvermedMsra { get; set; }
        public string MsraalieraCareSpa { get; set; }
        public string _500kaddOnSpa { get; set; }
        public string HealthShareMsra { get; set; }
        public string CatastrophicMsra { get; set; }
        public string MaximumTermSharingCp { get; set; }
        public string MsraalieraCare { get; set; }
        public string _500kaddOn { get; set; }
        public string Msrastm { get; set; }
        public string PolicyTerm { get; set; }
        public string Program_SRA { get; set; }
        public long? GroupId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long MemberSubscribedPlanId { get; set; }

        public virtual MemberSubscribedPlan MemberSubscribedPlan { get; set; }
    }
}
