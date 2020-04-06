using System;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSummaryRequestBO
    {
        public long BrokerId { get; set; }
        public int MemberId { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime SignedDate { get; set; }
        public string Body { get; set; }
        public PaymentDetailsBO Paymentdetails { get; set; }
        public PaymentBO Payment { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime FirstPaymentDate { get; set; }
        public int? PolicyTerm { get; set; }
    }
}