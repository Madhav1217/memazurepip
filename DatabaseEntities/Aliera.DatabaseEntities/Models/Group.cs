using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Group
    {
        public Group()
        {
            CommissionReport = new HashSet<CommissionReport>();
            GroupAddress = new HashSet<GroupAddress>();
            InvoiceAging = new HashSet<InvoiceAging>();
            Invoices = new HashSet<Invoices>();
            MemberSubscription = new HashSet<MemberSubscription>();
            Subgroup = new HashSet<Subgroup>();
        }

        public long GroupId { get; set; }
        public string Admin123Id { get; set; }
        public long? BrokerId { get; set; }
        public string GroupLabel { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string AlternatePhone { get; set; }
        public string Fax { get; set; }
        public string EmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ActiveDate { get; set; }
        public DateTime? InactiveDate { get; set; }
        public DateTime? RenewalDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<CommissionReport> CommissionReport { get; set; }
        public virtual ICollection<CommissionAllPayPeriodData> CommissionCalculatedReport { get; set; }
        public virtual ICollection<CommissionPayPeriodData> CommissionCalculatedReport90Days { get; set; }
        public virtual ICollection<GroupAddress> GroupAddress { get; set; }
        public virtual ICollection<InvoiceAging> InvoiceAging { get; set; }
        public virtual ICollection<Invoices> Invoices { get; set; }
        public virtual ICollection<MemberSubscription> MemberSubscription { get; set; }
        public virtual ICollection<Subgroup> Subgroup { get; set; }
    }
}
