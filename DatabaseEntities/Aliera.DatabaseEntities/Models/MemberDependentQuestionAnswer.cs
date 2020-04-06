using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberDependentQuestionAnswer
    {
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public int DepenedentQuestionnaireId { get; set; }
        public string DependentAnswer { get; set; }
        public long Id { get; set; }

        public virtual Member Member { get; set; }
        public virtual MemberDetail MemberDetail { get; set; }
    }
}
