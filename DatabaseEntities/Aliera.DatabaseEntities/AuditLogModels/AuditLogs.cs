using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.AuditLogModels
{
    public partial class AuditLogs
    {
        public long AuditLogId { get; set; }
        public string Action { get; set; }
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
    }
}
