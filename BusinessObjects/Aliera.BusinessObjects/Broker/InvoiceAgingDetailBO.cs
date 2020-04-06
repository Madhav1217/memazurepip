using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class InvoiceAgingDetailBO
    {
        public InvoiceAgingDetailBO()
        {
            InvoiceAgings = new List<InvoiceAgingBO>();
        }
        public int TotalCount { get; set; }
        public List<InvoiceAgingBO> InvoiceAgings { get; set; }
    }
}
