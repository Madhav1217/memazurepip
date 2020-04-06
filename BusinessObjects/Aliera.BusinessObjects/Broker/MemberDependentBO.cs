using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberDependentBO
    {
        public MemberDependentBO()
        {
            MemberDependentProductStatusBO = new List<MemberDependentProductStatusBO>();
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public RelationshipBO Relationship { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        //public List<string> ProductList { get; set; }
        public int Status { get; set; }
        //public DateTime? ActiveDate { get; set; }
        //public DateTime? InactiveDate { get; set; }
        //public DateTime? HoldDate { get; set; }
        //public DateTime? PendingDate { get; set; }
        public DateTime? DeactiveDate { get; set; }
        public long? AvatarImageId { get; set; }
        public long MemberDetailId { get; set; }
        public List<MemberDependentProductStatusBO> MemberDependentProductStatusBO { get; set; }
    }
}
