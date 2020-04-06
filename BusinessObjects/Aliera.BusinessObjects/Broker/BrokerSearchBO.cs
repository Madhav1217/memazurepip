using System;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerSearchBO
    {
        public string BrokerID { get; set; }
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNPN { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string EmailId { get; set; }
        public string Website { get; set; }
        public DateTime CreatedOn { get; set; }
        public BrokerStatus Status { get; set; }
        public string BrokerTypeName { get; set; }
        public long? ParentBrokerId { get; set; }
        public string ParentExternalId { get; set; }
        public string ParentBrokerName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsWebsiteActive { get; set; }
        public string IsWebsiteValue { get; set; }
        public int MemberCount { get; set; }
        public int ProductCount { get; set; }
        public DateTimeOffset? TrainingDate { get; set; }
    }
}
