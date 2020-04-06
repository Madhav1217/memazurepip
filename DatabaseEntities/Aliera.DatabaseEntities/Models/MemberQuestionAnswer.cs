using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberQuestionAnswer
    {
        public long MemberId { get; set; }
        public int MemberQuestionnaireId { get; set; }
        public string MemberAnswer { get; set; }

        public virtual Member Member { get; set; }
        public virtual MemberQuestionnaire MemberQuestionnaire { get; set; }
    }
}
