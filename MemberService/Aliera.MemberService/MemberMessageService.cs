using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class MemberMessageService : IMemberMessageService
    {
        private readonly IMemberMessageDataAccess _messageDataAccess;

        public MemberMessageService(IMemberMessageDataAccess messageDataAccess)
        {
            _messageDataAccess = messageDataAccess;
        }

        /// <summary>
        /// Gets the member messages.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="isArchived">if set to <c>true</c> [is archived].</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceGetMemberMessagesInputEmptyErrorCode</exception>
        public async Task<MessageDetailBO> GetMemberMessages(long memberId, bool isArchived, AuditLogBO auditLogBO)
        {
            if (memberId == 0)
                throw new CustomException(nameof(MemberConstants.MessageServiceGetMemberMessagesInputEmptyErrorCode));
            return await _messageDataAccess.GetMemberMessages(memberId, isArchived, auditLogBO);
        }

        /// <summary>
        /// Gets the member messages by page.
        /// </summary>
        /// <param name="memberMessageDetailBO">The member message detail bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceGetMemberMessagesByPageInputEmptyErrorCode</exception>
        public async Task<MessageDetailBO> GetMemberMessagesByPage(MessageDetailBO memberMessageDetailBO, AuditLogBO auditLogBO)
        {
            if (memberMessageDetailBO == null)
                throw new CustomException(nameof(MemberConstants.MessageServiceGetMemberMessagesByPageInputEmptyErrorCode));
            return await _messageDataAccess.GetMemberMessagesByPage(memberMessageDetailBO, auditLogBO);
        }

        /// <summary>
        /// Marks the message as read.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceMarkMessageAsReadInputEmptyErrorCode</exception>
        public async Task<int> MarkMessageAsRead(long memberMessageId, AuditLogBO auditLogBO)
        {
            if (memberMessageId == 0) throw new CustomException(nameof(MemberConstants.MessageServiceMarkMessageAsReadInputEmptyErrorCode));
            return await _messageDataAccess.MarkMessageAsRead(memberMessageId, auditLogBO);
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceDeleteMessageInputEmptyErrorCode</exception>
        public async Task<int> DeleteMessage(List<long> memberMessageId, AuditLogBO auditLogBO)
        {
            if (memberMessageId == null || memberMessageId.Count == 0)
                throw new CustomException(nameof(MemberConstants.MessageServiceDeleteMessageInputEmptyErrorCode));
            return await _messageDataAccess.DeleteMessage(memberMessageId, auditLogBO);
        }

        /// <summary>
        /// Marks the message as archived.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="isArchived">if set to <c>true</c> [is archived].</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceMarkMessageAsArchivedInputEmptyErrorCode</exception>
        public async Task<int> MarkMessageAsArchived(List<long> memberMessageId, bool isArchived, AuditLogBO auditLogBO)
        {
            if (memberMessageId == null || memberMessageId.Count == 0)
                throw new CustomException(nameof(MemberConstants.MessageServiceMarkMessageAsArchivedInputEmptyErrorCode));
            return await _messageDataAccess.MarkMessageAsArchived(memberMessageId, isArchived, auditLogBO);
        }

        /// <summary>
        /// Gets the unread message count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MessageServiceGetUnreadMessageCountInputEmptyErrorCode</exception>
        public async Task<int> GetUnreadMessageCount(long userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0)
                throw new CustomException(nameof(MemberConstants.MessageServiceGetUnreadMessageCountInputEmptyErrorCode));
            return await _messageDataAccess.GetUnreadMessageCount(userId, auditLogBO);
        }
    }
}