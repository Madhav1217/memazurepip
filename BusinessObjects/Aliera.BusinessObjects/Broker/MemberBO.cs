using System;
using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberBO
    {
        public string ExternalId { get; set; }
        public string MemberAdmin123Id { get; set; }
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public long BrokerId { get; set; }
        public string BrokerName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Group { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string ProductCategory { get; set; }
        public int DependentCount { get; set; }
        public string StateName { get; set; }
        public MemberAddressBO MemberAddress { get; set; }
        public string Product { get; set; }
        public DateTime? CreatedDate { get; set; }
        public MemberStatus PlanStatus { get; set; }
        public MemberStatus MemberStatus { get; set; }
        public int ActiveCount { get; set; }
        public int ActiveOnholdCount { get; set; }
        public int InActiveCount { get; set; }
        public int PendingCount { get; set; }
        public int UnderReviewCount { get; set; }
        public DemographicsBO Demographics { get; set; }
        public List<MemberAddressBO> Addresses { get; set; }
        public List<PlanBO> Plan { get; set; }        
    }
}
