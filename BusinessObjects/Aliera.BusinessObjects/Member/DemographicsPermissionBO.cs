using Aliera.BusinessObjects.Audit;

namespace Aliera.BusinessObjects.Member
{
    public class DemographicsPermissionBO
    {
        public int UserId { get; set; }
        public AuditLogBO AuditLogBO { get; set; }
        public int SubscriberInfoPermission { get; set; }
        public int DependentInfoPermission { get; set; }
        public int ServiceInfoPermission { get; set; }
        public int DigitalIdCardPermission { get; set; }
    }
}