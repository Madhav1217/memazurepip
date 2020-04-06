using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberSummaryProductListBO
    {
        public List<MemberSummaryProductBO> MemberPrimaryProductSummary { get; set; }
        public string MemberIdExternal { get; set; }
        public decimal? FirstMonthContribution { get; set; }
        public decimal? TotalRecurringContribution { get; set; }
    }

    public class MemberSummaryProductBO
    {
        public string ProductName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? DtEffective { get; set; }
        public int? PolicyTerm { get; set; }
        public int FamilyIndicator { get; set; }
        public string FamilyIndicatorValue { get; set; }
        public decimal? Premium { get; set; }
        public decimal? FamilyRiderFee { get; set; }
        public decimal? ToboccoFee { get; set; }
        public decimal? ApplicationFee { get; set; }
    }
}
