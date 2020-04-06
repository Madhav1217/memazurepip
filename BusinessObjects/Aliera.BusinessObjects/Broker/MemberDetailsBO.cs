using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberDetailsBO
    {
        public MemberDetailsBO()
        {
            MemerDependents = new List<MemberDependentBO>();
        }
        public string MemberId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int Age { get; set; }
        public int Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public DateTime? HoldDate { get; set; }
        public string HoldReason { get; set; }
        public string InactiveReason { get; set; }
        public string SignatureOnFile { get; set; }
        public string CancellationReason { get; set; }
        public string Cancelled { get; set; }
        public string Broker { get; set; }
        public List<MemberDependentBO> MemerDependents { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
    }
}
