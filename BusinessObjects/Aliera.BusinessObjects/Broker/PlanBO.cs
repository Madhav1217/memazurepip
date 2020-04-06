using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PlanBO
    {
        public int PlanId { get; set; }
        public decimal? ApplicationFee { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Premium { get; set; }
        public decimal? TotalPremium { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
        public DateTime? PlanEndDate { get; set; }
        public decimal? TobaccoRiderFee { get; set; }
        public decimal? FamilyRiderFee { get; set; }
        public int PlanType { get; set; }
        public long MemberSubscriptionId { get; set; }
        public decimal PackagedPlanPremium { get; set; }
        public List<MemberSummarySupplementalPlanDetailsBO> SupplementalPlans { get; set; }
        public List<PackagedPlanDetailsBO> PackagedPlans { get; set; }
        public decimal? TotalRecurringFee { get; set; }
        public double PlanRating { get; set; }
        public double CategoryWisePlanRating { get; set; }
        public bool IsPolicyTerm { get; set; }
        public int PolicyTerm { get; set; }
    }
}
