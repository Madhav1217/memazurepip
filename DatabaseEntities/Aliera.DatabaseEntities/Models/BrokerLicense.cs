using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class BrokerLicense
    {
        public string LicenseNumber { get; set; }
        public long BrokerId { get; set; }
        public string StateCode { get; set; }
        public DateTime ActiveDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string LicenseType { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
