using System;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberTransactionBO
    {
        public long MemberId { get; set; }
        public int PlanId { get; set; }
        public DateTime? Date { get; set; }
        public decimal? Amount { get; set; }
        public string Method { get; set; }
        public string Processor { get; set; }
        public string PlanName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? InformationDate { get; set; }
        public string Status { get; set; }
        public string StatusReason { get; set; }
    }
}
