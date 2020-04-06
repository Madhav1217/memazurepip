using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Subgroup
    {
        public Subgroup()
        {
            Division = new HashSet<Division>();
            MemberSubscription = new HashSet<MemberSubscription>();
        }

        public long SubgroupId { get; set; }
        public string Name { get; set; }
        public long? GroupId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual Group Group { get; set; }
        public virtual ICollection<Division> Division { get; set; }
        public virtual ICollection<MemberSubscription> MemberSubscription { get; set; }
    }
}
