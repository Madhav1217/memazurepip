using System;
using Microsoft.AspNetCore.Http;
using Aliera.Utilities;
using Aliera.Utilities.Enumerations;
using System.Linq;

namespace Aliera.BusinessObjects.Audit
{
    public class AuditLogBO
    {
        public AuditLogBO()
        {

        }

        public AuditLogBO(string applicationName, string jwt, IHttpContextAccessor httpContextAccessor)
        {
            ApplicationName = applicationName;
            UserId = UtilityHelper.GetUserId(jwt);
            UserName = UtilityHelper.GetUserName(jwt);
            UserIpaddress = UtilityHelper.GetIP(httpContextAccessor);
        }

        public long AuditLogId { get; set; }
        public AuditAction Action { get; set; }
        public string ApplicationName { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public DateTime AuditDate { get; set; }
        public string UserIpaddress { get; set; }
        public string EntityType { get; set; }
        public long? EntityTypeId { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public string Comments { get; set; }
        public string Module { get; set; }
    }
}
