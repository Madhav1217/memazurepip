using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberFeedbackBO
    {
        public long MemberFeedbackId { get; set; }
        public string MemberExternalId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int SourceId { get; set; }
        public long UserId { get; set; }
        public string InquiryTypeEmail { get; set; }
        public string InquiryType { get; set; }
    }
}