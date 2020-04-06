using Aliera.Utilities.Enumerations;
using System;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Member
{
    public class MemberRegistrationBO
    {
        public long MemberId { get; set; }
        public string ExternalId { get; set; }
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int MemberType { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime Dob { get; set; }
        public string ZipCode { get; set; }
        public int Mode { get; set; }
        public string ModeValue { get; set; }
        public IList<SecurityQuestionAnswersBO> SecurityQuestionAnswer { get; set; }
        public bool UpdateToAdmin123 { get; set; }
        public bool EmailNotFound { get; set; }
        public bool PhoneNumberNotFound { get; set; }
        public string UserName { get; set; }
        public UserStatus Status { get; set; }
        public EntityType UserType { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool LockoutEnabled { get; set; }
    }
}