using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSummaryPlanDetailsBO
    {
        public int ProductId { get; set; }
        public long? ProductCode { get; set; }
        public int PlanId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal? FirstMonthContribution { get; set; }
        public decimal? ApplicationFee { get; set; }
        public decimal? ToboccoFee { get; set; }
        public decimal? FamilyRiderFee { get; set; }
        public decimal Premium { get; set; }
        public decimal TotalRecurringContribution { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? MonthlyContributionDate { get; set; }
        public int FamilyIndicator { get; set; }
        public PackagedPlanDetailsBO PackagedPlan { get; set; }
        public string MSRA { get; set; }
        public string Duration { get; set; }
        public string Limit { get; set; }
        public int? PolicyTerm { get; set; }
        public string PolicyNumber { get; set; }
    }

    public class MemberSummaryPlanListBO
    {
        public List<MemberSummaryPlanDetailsBO> PrimaryPlans { get; set; }
        public List<MemberSummarySupplementalPlanDetailsBO> SupplementalPlans { get; set; }
    }
}
