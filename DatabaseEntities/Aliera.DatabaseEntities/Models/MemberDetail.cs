using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberDetail
    {
        public MemberDetail()
        {
            Member = new HashSet<Member>();
            MemberAccumulatorDetails = new HashSet<MemberAccumulatorDetails>();
            MemberDependent = new HashSet<MemberDependent>();
            MemberDependentAddress = new HashSet<MemberDependentAddress>();
            MemberDependentQuestionAnswer = new HashSet<MemberDependentQuestionAnswer>();
            MemberDependentStatusHistory = new HashSet<MemberDependentStatusHistory>();
        }

        public long MemberDetailId { get; set; }
        public string Salutation { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string PhoneNumber { get; set; }
        public string Ext { get; set; }
        public string EmailId { get; set; }
        public string Ssn { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int? PreferredLanguageId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? AvatarImageId { get; set; }
        public string MemberId123 { get; set; }
        public string MemberDependentId123 { get; set; }

        public virtual Language PreferredLanguage { get; set; }
        public virtual ICollection<Member> Member { get; set; }
        public virtual ICollection<MemberAccumulatorDetails> MemberAccumulatorDetails { get; set; }
        public virtual ICollection<MemberDependent> MemberDependent { get; set; }
        public virtual ICollection<MemberDependentAddress> MemberDependentAddress { get; set; }
        public virtual ICollection<MemberDependentQuestionAnswer> MemberDependentQuestionAnswer { get; set; }
        public virtual ICollection<MemberDependentStatusHistory> MemberDependentStatusHistory { get; set; }
    }
}
