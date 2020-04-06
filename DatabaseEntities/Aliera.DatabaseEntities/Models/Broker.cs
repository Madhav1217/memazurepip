using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Broker
    {
        public Broker()
        {
            AgentCommissionAgent = new HashSet<AgentCommission>();
            AgentCommissionPayToAgent = new HashSet<AgentCommission>();
            AgentProductApplicationFee = new HashSet<AgentProductApplicationFee>();
            AgentUserBridge = new HashSet<AgentUserBridge>();
            BrokerAddress = new HashSet<BrokerAddress>();
            BrokerContact = new HashSet<BrokerContact>();
            BrokerDocument = new HashSet<BrokerDocument>();
            BrokerHierarchyBroker = new HashSet<BrokerHierarchy>();
            BrokerHierarchyParentBroker = new HashSet<BrokerHierarchy>();
            BrokerPayPeriod = new HashSet<BrokerPayPeriod>();
            BrokerProduct = new HashSet<BrokerProduct>();
            CommissionReportMemberAgent = new HashSet<CommissionReport>();
            CommissionReportPayeeAgent = new HashSet<CommissionReport>();
            Eodetails = new HashSet<Eodetails>();
            MemberSubscription = new HashSet<MemberSubscription>();
            ProductFee = new HashSet<ProductFee>();
        }

        public long BrokerId { get; set; }
        public string Admin123Id { get; set; }
        public string ExternalId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public string Suffix { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalNpn { get; set; }
        public int? BrokerTypeId { get; set; }
        public string Company { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public bool? IsWebsiteActive { get; set; }
        public DateTimeOffset? TrainingDate { get; set; }
        public DateTime? TermDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string TaxId { get; set; }
        public bool IsVoiceVerification { get; set; }

        public virtual BrokerType BrokerType { get; set; }
        public virtual ICollection<AgentCommission> AgentCommissionAgent { get; set; }
        public virtual ICollection<AgentCommission> AgentCommissionPayToAgent { get; set; }
        public virtual ICollection<AgentProductApplicationFee> AgentProductApplicationFee { get; set; }
        public virtual ICollection<AgentUserBridge> AgentUserBridge { get; set; }
        public virtual ICollection<BrokerAddress> BrokerAddress { get; set; }
        public virtual ICollection<BrokerContact> BrokerContact { get; set; }
        public virtual ICollection<BrokerDocument> BrokerDocument { get; set; }
        public virtual ICollection<BrokerHierarchy> BrokerHierarchyBroker { get; set; }
        public virtual ICollection<BrokerHierarchy> BrokerHierarchyParentBroker { get; set; }
        public virtual ICollection<BrokerPayPeriod> BrokerPayPeriod { get; set; }
        public virtual ICollection<BrokerProduct> BrokerProduct { get; set; }
        public virtual ICollection<CommissionReport> CommissionReportMemberAgent { get; set; }
        public virtual ICollection<CommissionReport> CommissionReportPayeeAgent { get; set; }
        public virtual ICollection<Eodetails> Eodetails { get; set; }
        public virtual ICollection<MemberSubscription> MemberSubscription { get; set; }
        public virtual ICollection<ProductFee> ProductFee { get; set; }
    }
}
