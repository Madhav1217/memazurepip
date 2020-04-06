using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class PaymentReceiptFilterBO
    {
        public long UserId {get; set;}
        public DateTime? StartDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int RecordsPerPage { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }

    }
}
