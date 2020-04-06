using System;

namespace Aliera.BusinessObjects.Broker
{
    public class DemographicsBO
    {
        public long MemberDetailId { get; set; }
        public long MemberId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailId { get; set; }
        public string Ext { get; set; }
        public string Ssn { get; set; }
        public int? PrefferedLanguageId { get; set; }
        public bool IsTobaccoUser { get; set; }
        public int RelationshipId { get; set; }
        public string PrefferedLanguage { get; set; }
        public string TobaccoUser { get; set; }
        public string Relationship { get; set; }
        public string FamilyRiderOn { get; set; }
        public int Age { get; set; }
        public string MemberIdExternal { get; set; }

    }
}
