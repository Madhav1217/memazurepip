using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Eodetails
    {
        public int Eoid { get; set; }
        public long BrokerId { get; set; }
        public string Eocarrier { get; set; }
        public string EopolicyNumber { get; set; }
        public bool? IsEocoverage { get; set; }
        public DateTime? EoexpirationDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Eoamount { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Broker Broker { get; set; }
    }
}
