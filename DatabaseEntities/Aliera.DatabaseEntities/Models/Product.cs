using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Product
    {
        public Product()
        {
            AgentCommission = new HashSet<AgentCommission>();
            AgentProductApplicationFee = new HashSet<AgentProductApplicationFee>();
            BrokerProduct = new HashSet<BrokerProduct>();
            MemberPaymentDetail = new HashSet<MemberPaymentDetail>();
            Plan = new HashSet<Plan>();
            ProductAccumulator = new HashSet<ProductAccumulator>();
            ProductFee = new HashSet<ProductFee>();
            ProductProviderNetwork = new HashSet<ProductProviderNetwork>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }
        public int? ProductCategoryId { get; set; }
        public int? ProductSubCategoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? TermDate { get; set; }
        public string ProductLogo { get; set; }
        public string ExcludedSaleStates { get; set; }
        public decimal? ApplicationFee { get; set; }
        public int? ProductCode { get; set; }
        public string DocumentPath { get; set; }
        public string Description { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? DocumentId { get; set; }
        public decimal FamilyRiderFee { get; set; }
        public decimal TobaccoUserFee { get; set; }
        public string ShellSheetName { get; set; }
        public bool CanPayNextDay { get; set; }
        public bool IsGroupProduct { get; set; }
        public string ProductLabel { get; set; }
        public bool IsPolicyTerm { get; set; }
        public string Pcp { get; set; }
        public string UrgentCare { get; set; }
        public string Speciality { get; set; }
        public string Er { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ProductSubCategory ProductSubCategory { get; set; }
        public virtual ICollection<AgentCommission> AgentCommission { get; set; }
        public virtual ICollection<AgentProductApplicationFee> AgentProductApplicationFee { get; set; }
        public virtual ICollection<BrokerProduct> BrokerProduct { get; set; }
        public virtual ICollection<MemberPaymentDetail> MemberPaymentDetail { get; set; }
        public virtual ICollection<Plan> Plan { get; set; }
        public virtual ICollection<ProductAccumulator> ProductAccumulator { get; set; }
        public virtual ICollection<ProductFee> ProductFee { get; set; }
        public virtual ICollection<ProductProviderNetwork> ProductProviderNetwork { get; set; }
    }
}
