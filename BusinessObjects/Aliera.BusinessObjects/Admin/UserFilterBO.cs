using Aliera.Utilities.Enumerations;
using System.Collections.Generic;

namespace Aliera.BusinessObjects.Admin
{
    public class UserFilterBO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public long RoleId { get; set; }
        public string EmailId { get; set; }
        public SearchStatus Status { get; set; }
        public List<UserSearchBO> UserList { get; set; }
        public string SortColumn { get; set; }
        public bool IsSortByDesc { get; set; }
        public int PageNumber { get; set; }
        public int RecordsPerPage { get; set; }
        public int TotalRecords { get; set; }
        public SearchBy SearchBy { get; set; }
    }
}
