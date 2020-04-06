using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.DatabaseEntities.Models
{
    public class CommissionPayPeriodReport
    {
        public DateTime? PostedDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Type { get; set; }
        public string Subtype { get; set; }
        public string Notes { get; set; }        
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal Total { get; set; }
        public string PayeeAgentId { get; set; }
        public string PayeeBrokerCompany { get; set; }
        public string PayeeAgentLabel { get; set; }
        public string PayeeAddress1 { get; set; }
        public string PayeeAddress2 { get; set; }
        public string PayeeCity { get; set; }
        public string PayeeState { get; set; }
        public string PayeeZipcode { get; set; }
        public string MemberId { get; set; }
        public string MemberCompany { get; set; }
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string MemberAgentId { get; set; }
        public string MemberAgentCompany { get; set; }
        public string MemberAgentLabel { get; set; }
        public string MemberAgentCategory { get; set; }
        public string MemberSourceDetail { get; set; }
        public string MemberLeadStatus { get; set; }
        public string ProductId { get; set; }
        public string ProductLabel { get; set; }
        public string ProductCategory { get; set; }
        public string ProductCategory2 { get; set; }
        public string ProductCode { get; set; }
        public string ProductCreatedDate { get; set; }
        public string ProductActiveDate { get; set; }
        public string TransactionId { get; set; }
        public string TransactionAmount { get; set; }
        public string TransactionType { get; set; }
        public string TransactionPayment { get; set; }
        public string TransactionPaidThroughStart { get; set; }
        public string TransactionPaidThroughEnd { get; set; }
        public string CommissionableAmount { get; set; }
        public string Payout { get; set; }
        public long Id { get; set; }
        public string GroupId { get; set; }
        public string PayPeriod { get; set; }
        public string Status { get; set; }
    }
}
