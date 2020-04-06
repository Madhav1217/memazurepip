using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class MemberFeedback
    {
        public long MemberFeedbackId { get; set; }
        public string MemberId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int SourceId { get; set; }
        public DateTime CreatedOn { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public string InquiryTypeEmail { get; set; }
        public string InquiryType { get; set; }
    }
}