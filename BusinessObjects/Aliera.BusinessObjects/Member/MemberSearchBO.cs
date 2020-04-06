namespace Aliera.BusinessObjects.Member
{
    public class MemberSearchBO
    {
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNumber { get; set; }
        public int ZipCode { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}