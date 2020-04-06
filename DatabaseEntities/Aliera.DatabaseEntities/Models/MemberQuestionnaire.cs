using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberQuestionnaire
    {
        public MemberQuestionnaire()
        {
            ConsentMemberQuestionAnswer = new HashSet<ConsentMemberQuestionAnswer>();
            MemberQuestionAnswerNavigation = new HashSet<MemberQuestionAnswer>();
        }

        public int MemberQuestionnaireId { get; set; }
        public int MemberQuestionnaireType { get; set; }
        public int? MemberQuestionDependentId { get; set; }
        public string MemberQuestionnaire1 { get; set; }
        public string MemberQuestionAnswer { get; set; }

        public virtual ICollection<ConsentMemberQuestionAnswer> ConsentMemberQuestionAnswer { get; set; }
        public virtual ICollection<MemberQuestionAnswer> MemberQuestionAnswerNavigation { get; set; }
    }
}
