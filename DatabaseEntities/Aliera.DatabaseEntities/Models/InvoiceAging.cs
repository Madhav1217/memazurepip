using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class InvoiceAging
    {
        public long InvoiceAgingId { get; set; }
        public string InvoiceTo { get; set; }
        public long GroupId { get; set; }
        public long BrokerId { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Days30 { get; set; }
        public decimal Days60 { get; set; }
        public decimal Days90 { get; set; }
        public decimal DayMoreThan90 { get; set; }

        public virtual Group Group { get; set; }
    }
}
