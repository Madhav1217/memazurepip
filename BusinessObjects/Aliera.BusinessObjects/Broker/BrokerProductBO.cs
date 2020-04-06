
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class BrokerProductBO
    {
        public string ProductStatus { get; set; }
        public string ProductName { get; set; }
        public string ProductDetail { get; set; }
        public int PlanId { get; set; }
        public int ProductId { get; set; }
        public string PlanName { get; set; }
        public int? PlanCode { get; set; }
        public string ApplicationFee { get; set; }
        public string Commission { get; set; }
        public string NoSaleStates { get; set; }
        public decimal? MSRA { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int FamilyIndicator { get; set; }
        public decimal Premium { get; set; }

        public string Individual { get; set; }
        public string IndividualPlusSpouse { get; set; }
        public string IndividualPlusChild { get; set; }
        public string Family { get; set; }
        public List<ProductFeesBO> PlanFees { get; set; }
    }

    public class ProductFeesBO
    {
        public string ProductFees { get; set; }
    }
}
