using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class AgentCommission
    {
        public long AgentCommissionId { get; set; }
        public string Parent { get; set; }
        public long? AgentId { get; set; }
        public string AgentLabel { get; set; }
        public string AgentActive { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Company { get; set; }
        public string TreeCount { get; set; }
        public int? ProductId { get; set; }
        public string ProductLabel { get; set; }
        public string Commissionable { get; set; }
        public long? PayToAgentId { get; set; }
        public string PayToAgentLabel { get; set; }
        public bool? PayToAgentActive { get; set; }
        public string Amount { get; set; }
        public string AmountType { get; set; }
        public string PriceTypes { get; set; }
        public string PriceIds { get; set; }
        public DateTime? MemberActiveStartDate { get; set; }
        public DateTime? MemberActiveEndDate { get; set; }
        public DateTime? BillingStartDate { get; set; }
        public DateTime? BillingEndDate { get; set; }
        public DateTime? TransactionStartDate { get; set; }
        public DateTime? TransactionEndDate { get; set; }
        public string Advancement { get; set; }
        public string AdvancementMonths { get; set; }
        public string PayOnPaymentNumber { get; set; }
        public string IncludeforPaymentNumber { get; set; }
        public string ExcludeforPaymentNumber { get; set; }
        public string IncludePriceTypes { get; set; }
        public string ExcludePriceTypes { get; set; }
        public string IncludeAllPriceTypes { get; set; }
        public string ExcludeAllPriceTypes { get; set; }
        public string WebsiteDisplayActive { get; set; }
        public DateTime? CommissionStart { get; set; }
        public DateTime? CommissionEnd { get; set; }

        public virtual Broker Agent { get; set; }
        public virtual Broker PayToAgent { get; set; }
        public virtual Product Product { get; set; }
    }
}
