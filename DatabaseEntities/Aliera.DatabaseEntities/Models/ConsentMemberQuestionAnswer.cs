using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class ConsentMemberQuestionAnswer
    {
        public long ConsentMemberQuestionAnswerId { get; set; }
        public long MemberId { get; set; }
        public int MemberQuestionnaireId { get; set; }
        public string MemberAnswer { get; set; }
        public DateTime ChangedDateTime { get; set; }
        public long CreatedBy { get; set; }

        public virtual Member Member { get; set; }
        public virtual MemberQuestionnaire MemberQuestionnaire { get; set; }
    }
}
