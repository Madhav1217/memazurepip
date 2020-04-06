using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberDependent
    {
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public bool IsTobaccoUser { get; set; }
        public string ExternalId { get; set; }
        public string Admin123Id { get; set; }
        public int RelationshipId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? UserId { get; set; }
        public DateTime? DeactiveDate { get; set; }
        public long? DigitalIdcardId { get; set; }

        public virtual Member Member { get; set; }
        public virtual MemberDetail MemberDetail { get; set; }
    }
}
