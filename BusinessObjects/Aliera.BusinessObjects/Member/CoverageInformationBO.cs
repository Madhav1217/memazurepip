using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class CoverageInformationBO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberName { get; set; }
        public string MemberType { get; set; }
        public string DependentRelationship { get; set; }
        public List<string> PlanName { get; set; }
        public List<string> Product { get; set; }
        public string LevelOfCoverage { get; set; }
        public DateTime? PaidThroughDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long DependentDetailId { get; set; }
        public string DependentStatus { get; set; }
        public long? DigitalCardId { get; set; }
        public long? AvatarImageId { get; set; }
    }
}
