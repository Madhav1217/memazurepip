namespace Aliera.DatabaseEntities.Models
{
    public partial class CommissionPayPeriodData
    {
        public long Id { get; set; }
        public long MemberId { get; set; }
        public string Subtype { get; set; }
        public string Debit { get; set; }
        public string Credit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public long? MemberBrokerId { get; set; }
        public string MemberExternalId { get; set; }
        public long? GroupId { get; set; }
        public string GroupExternalId { get; set; }
        public string MemberBrokerCompany { get; set; }
        public string PayeeBrokerCompany { get; set; }
        public string Refunds { get; set; }
        public string Premium { get; set; }
        public string Status { get; set; }
        public string Commission { get; set; }
        public long? PayeeBrokerId { get; set; }
        public string PayeeExternalId { get; set; }

        public virtual Group Group { get; set; }
    }
}
