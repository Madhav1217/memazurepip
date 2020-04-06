using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerContact
    {
        public long BrokerContactId { get; set; }
        public long BrokerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Ext { get; set; }
        public string PhoneNumberAlternate { get; set; }
        public string BestTimeToCall { get; set; }
        public string FaxNumber { get; set; }
        public string EmailId { get; set; }
        public string EmailIdAlternate { get; set; }
        public string Website { get; set; }
        public bool? DoNotCall { get; set; }
        public bool? EmailOptOut { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Broker Broker { get; set; }
    }
}
