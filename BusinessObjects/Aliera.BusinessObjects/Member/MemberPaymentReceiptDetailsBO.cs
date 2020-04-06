using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberPaymentReceiptDetailsBO
    {
        public string TransactionId { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? Amount { get; set; }
        public string PaymentMethod { get; set; }
        public long ReceiptId { get; set; }
        public int TotalRecordsCount { get; set; }
        public string Status { get; set; }

    }
}


