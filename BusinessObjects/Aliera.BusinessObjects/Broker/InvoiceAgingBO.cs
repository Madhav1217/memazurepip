using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class InvoiceAgingBO
    {
        public long InvoiceAgingId { get; set; }
        public string InvoiceTo { get; set; }
        public string GroupName { get; set; }
        public decimal LastInvoiceAmount { get; set; }
        public long BrokerId { get; set; }
        public decimal TotalDue { get; set; }
        public decimal Days30 { get; set; }
        public decimal Days60 { get; set; }
        public decimal Days90 { get; set; }
        public decimal DayMoreThan90 { get; set; }
        public long DocumentId { get; set; }
    }
}
