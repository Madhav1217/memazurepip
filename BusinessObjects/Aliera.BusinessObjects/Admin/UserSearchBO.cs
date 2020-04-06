using Aliera.Utilities.Enumerations;
using System;

namespace Aliera.BusinessObjects.Admin
{
    public class UserSearchBO
    {
        public string ExternalId { get; set; }
        public long? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string RoleName { get; set; }
        public SearchStatus Status { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
