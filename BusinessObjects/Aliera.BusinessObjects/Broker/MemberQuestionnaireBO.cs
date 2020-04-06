namespace Aliera.BusinessObjects.Broker
{
    public class MemberQuestionnaireBO
    {
        public int MemberQuestionnaireId { get; set; }
        public int MemberQuestionnaireType { get; set; }
        public int? MemberQuestionDependentId { get; set; }
        public string MemberQuestionnaire { get; set; }
        public string MemberQuestionAnswer { get; set; }
    }
}
