using System;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSummarySupplementalPlanDetailsBO
    {
        public long? ProductCode { get; set; }
        public int ProductId { get; set; }
        public int PlanId { get; set; }
        public string ProductName { get; set; }
        public int PlanType { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? FirstMonthContribution { get; set; }
        public decimal TotalRecurringContribution { get; set; }
        public int FamilyIndicator { get; set; }
        public string MSRA { get; set; }
        public string Duration { get; set; }
        public string Limit { get; set; }
        public DateTime? MonthlyContributionDate { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public string PolicyNumber { get; set; }
        public decimal Premium { get; set; }


    }
}
