using System;

namespace Aliera.BusinessObjects.Member
{
    public class DependentInformationBO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MemberName { get; set; }
        public string DependentRelationship { get; set; }
        public DateTime? PaidThroughDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long DependentDetailId { get; set; }
        public string DependentStatus { get; set; }
        public long? AvatarImageId { get; set; }
    }
}