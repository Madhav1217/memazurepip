using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class InvoiceDetailBO
    {
        public InvoiceDetailBO()
        {
            Invoices = new List<InvoiceBO>();
        }
        public int TotalCount { get; set; }
        public List<InvoiceBO> Invoices { get; set; }
    }
}
