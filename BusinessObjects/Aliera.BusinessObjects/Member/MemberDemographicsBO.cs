using Aliera.BusinessObjects.Broker;
using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Member
{
    public class MemberDemographicsBO
    {
        public long MemberID { get; set; }
        public string ExternalMemberId { get; set; }

        public long? GroupID { get; set; }

        public long? SubGroupID { get; set; }

        public long? DivisionID { get; set; }

        public DateTime? PaidThroughDate { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateofBirth { get; set; }

        public string Gender { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string StateProvince { get; set; }

        public string ZipCode { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string MemberType { get; set; }

        public long MemberDetailId { get; set; }
        public long? AvatarImageId { get; set; }

        public List<DependentInformationBO> DependentInformation { get; set; }
        public List<ServiceInformationBO> ServiceInformation { get; set; }

        public List<MemberAddressBO> MemberAddress { get; set; }
        public MemberDetailsBO MemberDetails { get; set; }

        public DependentDetailsBO DependentDetails { get; set; }
    }
}