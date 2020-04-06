using System;

namespace Aliera.BusinessObjects.Broker
{
    public class ConsentMemberQuestionAnswersBO
    {
        public int MemberQuestionnaireId { get; set; }
        public int MemberQuestionnaireType { get; set; }
        public string MemberAnswer { get; set; }
        public DateTime ChangedDateTime { get; set; }
    }
}
