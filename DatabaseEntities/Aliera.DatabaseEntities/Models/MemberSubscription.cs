using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberSubscription
    {
        public MemberSubscription()
        {
            MemberSubscribedPlan = new HashSet<MemberSubscribedPlan>();
        }

        public long MemberSubscriptionId { get; set; }
        public long MemberId { get; set; }
        public long BrokerId { get; set; }
        public decimal? TotalPremium { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? FirstPaymentDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string NotificationEmail { get; set; }
        public string NotificationPhone { get; set; }
        public DateTime? NotificationDate { get; set; }
        public int? PaymentType { get; set; }
        public DateTime? SignedDate { get; set; }
        public string RoutingNumber { get; set; }
        public string CardOrAccountNumber { get; set; }
        public string VerificationCode { get; set; }
        public string Browser { get; set; }
        public string Ipaddress { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? GroupId { get; set; }
        public long? SubgroupId { get; set; }
        public long? DivisionId { get; set; }
        public int? CardType { get; set; }
        public long? DocumentId { get; set; }

        public virtual Broker Broker { get; set; }
        public virtual Division Division { get; set; }
        public virtual Group Group { get; set; }
        public virtual Member Member { get; set; }
        public virtual Subgroup Subgroup { get; set; }
        public virtual ICollection<MemberSubscribedPlan> MemberSubscribedPlan { get; set; }
    }
}
