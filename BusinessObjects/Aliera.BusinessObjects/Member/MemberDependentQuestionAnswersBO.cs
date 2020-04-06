using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberDependentQuestionAnswersBO
    {
        public long MemberId { get; set; }
        public long MemberDetailId { get; set; }
        public int DependentQuestionnaireId { get; set; }
        public string DependentAnswer { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
