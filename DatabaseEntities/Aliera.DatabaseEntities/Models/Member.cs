using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Member
    {
        public Member()
        {
            CommissionReport = new HashSet<CommissionReport>();
            ConsentMemberQuestionAnswer = new HashSet<ConsentMemberQuestionAnswer>();
            MemberAccumulatorDetails = new HashSet<MemberAccumulatorDetails>();
            MemberAddress = new HashSet<MemberAddress>();
            MemberDependent = new HashSet<MemberDependent>();
            MemberDependentAddress = new HashSet<MemberDependentAddress>();
            MemberDependentQuestionAnswer = new HashSet<MemberDependentQuestionAnswer>();
            MemberDependentStatusHistory = new HashSet<MemberDependentStatusHistory>();
            MemberPaymentDetail = new HashSet<MemberPaymentDetail>();
            MemberQuestionAnswer = new HashSet<MemberQuestionAnswer>();
            MemberSubscription = new HashSet<MemberSubscription>();
        }

        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public int Status { get; set; }
        public string ExternalId { get; set; }
        public string Admin123Id { get; set; }
        public bool IsTobaccoUser { get; set; }
        public bool IsFamilyRiderOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? UserId { get; set; }
        public long? DigitalIdcardId { get; set; }

        public virtual MemberDetail MemberDetail { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<CommissionReport> CommissionReport { get; set; }
        public virtual ICollection<ConsentMemberQuestionAnswer> ConsentMemberQuestionAnswer { get; set; }
        public virtual ICollection<MemberAccumulatorDetails> MemberAccumulatorDetails { get; set; }
        public virtual ICollection<MemberAddress> MemberAddress { get; set; }
        public virtual ICollection<MemberDependent> MemberDependent { get; set; }
        public virtual ICollection<MemberDependentAddress> MemberDependentAddress { get; set; }
        public virtual ICollection<MemberDependentQuestionAnswer> MemberDependentQuestionAnswer { get; set; }
        public virtual ICollection<MemberDependentStatusHistory> MemberDependentStatusHistory { get; set; }
        public virtual ICollection<MemberPaymentDetail> MemberPaymentDetail { get; set; }
        public virtual ICollection<MemberQuestionAnswer> MemberQuestionAnswer { get; set; }
        public virtual ICollection<MemberSubscription> MemberSubscription { get; set; }
    }
}
