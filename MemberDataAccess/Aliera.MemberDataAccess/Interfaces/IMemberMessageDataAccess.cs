using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public interface IMemberMessageDataAccess
    {
        Task<MessageDetailBO> GetMemberMessages(long userId, bool isArchived, AuditLogBO auditLogBO);

        Task<int> MarkMessageAsRead(long memberMessageId, AuditLogBO auditLogBO);

        Task<int> MarkMessageAsArchived(List<long> memberMessageId, bool isArchived, AuditLogBO auditLogBO);

        Task<int> DeleteMessage(List<long> memberMessageId, AuditLogBO auditLogBO);

        Task<MessageDetailBO> GetMemberMessagesByPage(MessageDetailBO memberMessageDetailBO, AuditLogBO auditLogBO);

        Task<int> GetUnreadMessageCount(long userId, AuditLogBO auditLogBO);
    }
}