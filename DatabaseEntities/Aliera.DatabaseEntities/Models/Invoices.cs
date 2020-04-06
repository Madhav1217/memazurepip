using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Invoices
    {
        public long InvoiceId { get; set; }
        public long GroupId { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceTo { get; set; }
        public string Method { get; set; }
        public string Delivery { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfProducts { get; set; }
        public int NumberOfMembers { get; set; }
        public decimal Total { get; set; }
        public decimal Due { get; set; }
        public decimal Paid { get; set; }
        public decimal PastDue { get; set; }
        public DateTime? PayDate { get; set; }
        public string FileId { get; set; }

        public virtual Group Group { get; set; }
    }
}
