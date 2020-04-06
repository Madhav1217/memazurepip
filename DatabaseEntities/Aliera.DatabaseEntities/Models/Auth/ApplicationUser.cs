using Aliera.Utilities.Enumerations;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aliera.DatabaseEntities.Models.Auth
{
    public class ApplicationUser : IdentityUser<long>
    {
        public ApplicationUser()
        {
            UserRoleAssociation = new HashSet<UserRoleAssociation>();
        }
        public EntityType UserType { get; set; }
        public DateTime LastPasswordChangedDate { get; set; }
        public DateTimeOffset? LastLogin { get; set; }
        public DateTimeOffset? CurrentLogin { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int PortalId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public bool IsFirstLogin { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleAssociation> UserRoleAssociation { get; set; }
    }
}
