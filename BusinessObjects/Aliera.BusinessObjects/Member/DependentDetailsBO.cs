using Aliera.BusinessObjects.Broker;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class DependentDetailsBO
    {
       public string  FirstName { get; set; }
       public string  LastName  { get; set; }
       public string  EmailId  { get; set; }
       public string  PhoneNumber  { get; set; }
       public string  Gender { get; set; }
       public string  DOB  { get; set; }
       public string PrefferedLanguage  { get; set; }
       public string Ssn  { get; set; }
       public bool isTobaccoUser { get; set; }
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public int AddressTypeId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int RelationshipId { get; set; }
        public string Relationship { get; set; }
        public List<MemberDependentQuestionAnswersBO> MemberDependentQuestionAnswersBO { get; set; }
        public MemberDemographicsBO MemberDemographicsBO { get; set; }
    }
}
