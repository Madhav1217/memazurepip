using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Division
    {
        public Division()
        {
            MemberSubscription = new HashSet<MemberSubscription>();
        }

        public long DivisionId { get; set; }
        public string Name { get; set; }
        public long? SubgroupId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Subgroup Subgroup { get; set; }
        public virtual ICollection<MemberSubscription> MemberSubscription { get; set; }
    }
}
