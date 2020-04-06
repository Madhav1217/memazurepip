namespace Aliera.BusinessObjects.Broker
{
    public class LineChartBO
    {
        public string Period { get; set; }
        public int EnrolledMemberCount { get; set; }
        public string EnrolledExternalIds { get; set; }
        public int CancelledMemberCount { get; set; }
        public string CancelledExternalIds { get; set; }
    }
}
