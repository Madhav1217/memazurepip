namespace Aliera.BusinessObjects.Broker
{
    public class PlanParametersBO
    {
        public int MemberAge { get; set; }
        public int NoOfDependents { get; set; }
        public int NoOfTobaccoUser { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public int FamilyIndicator { get; set; }
        public bool IsSpouse { get; set; }
        public bool IsChild { get; set; }
        public int PrimaFamilyIndicator { get; set; }
        public int NonPrimaFamilyIndicator { get; set; }
    }
}
