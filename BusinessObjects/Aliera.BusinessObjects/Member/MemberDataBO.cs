using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberDataBO
    {
        public string ExternalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? UserId { get; set; }
        public string FullName { get; set; }
        public string MemberType { get; set; }
        public int Status { get; set; }
        public long MemberDetailId { get; set; }
        public long MemberId { get; set; }
        public long BrokerId { get; set; }
        public long? GroupId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string State { get; set; }
    }
}