﻿using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class GroupAddress
    {
        public long GroupAddressId { get; set; }
        public long GroupId { get; set; }
        public bool IsPrimary { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Group Group { get; set; }
    }
}
