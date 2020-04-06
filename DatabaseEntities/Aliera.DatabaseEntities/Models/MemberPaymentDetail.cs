using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberPaymentDetail
    {
        public long MemberPaymentDetailId { get; set; }
        public long MemberId { get; set; }
        public string InvoiceId { get; set; }
        public string TransactionId { get; set; }
        public DateTime? BillDate { get; set; }
        public DateTime? PaidDate { get; set; }
        public decimal? InvoicedAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public string Processor { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
        public string Information { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ProductId { get; set; }
        public long? PaymentNumber { get; set; }

        public virtual Member Member { get; set; }
        public virtual Product Product { get; set; }
    }
}
