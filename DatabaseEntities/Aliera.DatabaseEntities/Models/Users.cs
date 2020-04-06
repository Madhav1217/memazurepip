using Aliera.Utilities.Enumerations;
using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class Users
    {
        public Users()
        {
            UserSecurityQuestionAnswers = new HashSet<UserSecurityQuestionAnswers>();
        }

        public long UserId { get; set; }
        public string UserName { get; set; }
        public EntityType UserType { get; set; }
        public string Email { get; set; }
        public bool IsEmailVerified { get; set; }
        public string PasswordHash { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public int PortalId { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public DateTimeOffset? CurrentLogin { get; set; }
        public string NormalizedUserName { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public bool? PhoneNumberConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string NormalizedEmail { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public UserStatus Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsFirstLogin { get; set; }

        public virtual AgentUserBridge AgentUserBridge { get; set; }

        public virtual Member Member { get; set; }
        public virtual ICollection<UserSecurityQuestionAnswers> UserSecurityQuestionAnswers { get; set; }
    }
}
