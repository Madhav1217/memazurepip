using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.ExtensionMethods;
using Portals = Aliera.Utilities.Enumerations.Portals;

namespace Aliera.MemberDataAccess
{
    public class MemberMessageDataAccess : IMemberMessageDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberMessageDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the member messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isArchived">if set to <c>true</c> [is archived].</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<MessageDetailBO> GetMemberMessages(long userId, bool isArchived, AuditLogBO auditLogBO)
        {
            var memberMessageDetailBO = await GetMemberAllMessages(userId);
            memberMessageDetailBO.Messages = memberMessageDetailBO.Messages.Where(msg => msg.IsArchived == isArchived).ToList();

            //Log audit for select action on memberMessageDetailBO
            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return memberMessageDetailBO;
        }

        /// <summary>
        /// Gets the unread message count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> GetUnreadMessageCount(long userId, AuditLogBO auditLogBO)
        {
            var count = 0;
            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(predicate: m => m.UserId == userId);
            if (member == null)
                return count;

            var memberMessages = await _unitOfWork.GetRepository<Messages>().GetPagedListAsync(predicate:
                msg => msg.PortalId == (int)Portals.MemberPortal
                 && msg.RecipientId == member.MemberId && !msg.IsRead && (msg.IsArchived.HasValue && msg.IsArchived.Value == false || !msg.IsArchived.HasValue),
                pageIndex: 0, pageSize: int.MaxValue);
            count = memberMessages.Items.Count;

            //Log audit for update action on MemberMessage
            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);

            return count;
        }

        /// <summary>
        /// Marks the message as read.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> MarkMessageAsRead(long memberMessageId, AuditLogBO auditLogBO)
        {
            var rows = 0;
            var memberMessageRepo = _unitOfWork.GetRepository<Messages>();
            var memberMessage = await memberMessageRepo.FindAsync(memberMessageId);
            var existingMemberMessage = new Messages();
            existingMemberMessage = existingMemberMessage.Clone(memberMessage);

            if (memberMessage != null)
            {
                memberMessage.IsRead = true;
                rows = _unitOfWork.SaveChanges();
            }

            //Log audit for update action on MemberMessage
            await AuditMapper.AuditLogging(auditLogBO, memberMessageId, AuditAction.Update, null);
            return rows;
        }

        /// <summary>
        /// Marks the message as archived.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="isArchived">if set to <c>true</c> [is archived].</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> MarkMessageAsArchived(List<long> memberMessageId, bool isArchived, AuditLogBO auditLogBO)
        {
            var rows = 0;
            var memberMessage = await _unitOfWork.GetRepository<Messages>().GetPagedListAsync(predicate:
                msg => msg.PortalId == (int)Portals.MemberPortal &&
                memberMessageId.Contains(msg.MessageId), pageIndex: 0, pageSize: int.MaxValue);

            var existingMemberMessages = new List<Messages>();
            existingMemberMessages = existingMemberMessages.Clone(memberMessage.Items.ToList());

            if (memberMessage != null && memberMessage.Items.Any())
            {
                memberMessage.Items.All(item => { item.IsArchived = isArchived; return true; });
                _unitOfWork.GetRepository<Messages>().Update(memberMessage.Items);
                rows = await _unitOfWork.SaveChangesAsync();
            }

            //Log audit for update action on MemberMessage
            await AuditMapper.AuditLogging(auditLogBO, memberMessageId[0], AuditAction.Update, null);
            return rows;
        }

        /// <summary>
        /// Deletes the message.
        /// </summary>
        /// <param name="memberMessageId">The member message identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> DeleteMessage(List<long> memberMessageId, AuditLogBO auditLogBO)
        {
            var rows = 0;
            var memberMessage = await _unitOfWork.GetRepository<Messages>().GetPagedListAsync(predicate:
                msg => msg.PortalId == (int)Portals.MemberPortal &&
                memberMessageId.Contains(msg.MessageId), pageIndex: 0, pageSize: int.MaxValue);

            var existingMemberMessages = new List<Messages>();
            existingMemberMessages = existingMemberMessages.Clone(memberMessage.Items.ToList());

            if (memberMessage != null && memberMessage.Items.Any())
            {
                _unitOfWork.GetRepository<Messages>().Delete(memberMessage.Items);
                rows = await _unitOfWork.SaveChangesAsync();
            }

            //Log audit for update action on MemberMessage
            await AuditMapper.AuditLogging(auditLogBO, memberMessageId[0], AuditAction.Delete, null);

            return rows;
        }

        /// <summary>
        /// Gets the member messages by page.
        /// </summary>
        /// <param name="memberMessageDetailBO">The member message detail bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<MessageDetailBO> GetMemberMessagesByPage(MessageDetailBO memberMessageDetailBO, AuditLogBO auditLogBO)
        {
            var memberMessageBO = await GetMemberAllMessages(memberMessageDetailBO.UserId);
            //fetch message according to page number and page size
            if (memberMessageDetailBO.SortColumn == "unread")
            {
                var unreadMessages = memberMessageBO.Messages.Where(m => !m.IsRead).ToList();
                var readMsgs = memberMessageBO.Messages.Where(m => m.IsRead).ToList();
                memberMessageBO.Messages = unreadMessages.Concat(readMsgs).ToList();
            }
            memberMessageBO.Messages = memberMessageBO.Messages.Where(msg => msg.IsArchived == memberMessageDetailBO.IsArchivedMessageRequest)
                .Skip((memberMessageDetailBO.PageNumber - 1) * memberMessageDetailBO.MessagesPerPage).Take(memberMessageDetailBO.MessagesPerPage).ToList();

            //Log audit for select action on MemberDemographicsBO
            //await AuditMapper.AuditLogging(auditLogBO, memberMessageDetailBO.UserId, AuditAction.Select, null);

            return memberMessageBO;
        }

        /// <summary>
        /// Gets the member all messages.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<MessageDetailBO> GetMemberAllMessages(long userId)
        {
            var memberMessageDetailBO = new MessageDetailBO
            {
                Messages = new List<MessageBO>()
            };

            var member = await _unitOfWork.GetRepository<Member>()
                .GetFirstOrDefaultAsync(predicate: m => m.UserId == userId, include:i=> i
                .Include(a=>a.MemberDetail));
            

            if (member == null)
                return memberMessageDetailBO;

            var memberMessages = await _unitOfWork.GetRepository<Messages>().GetPagedListAsync(predicate:
                msg => msg.PortalId == (int)Portals.MemberPortal
                 && msg.RecipientId == member.MemberId,
                pageIndex: 0, pageSize: int.MaxValue);
            
            var messageBOs = memberMessages.Items.Select(message => new MessageBO()
                {
                    MessageId = message.MessageId,
                    IsRead = message.IsRead,
                    IsArchived = message.IsArchived ?? false,
                    Message = message.Message,
                    MessageSentTime = message.MessageSentTime,
                    SenderEmailId = member.MemberDetail.EmailId
                }).ToList();

            memberMessageDetailBO.Messages = messageBOs.OrderByDescending(m => m.MessageSentTime).ToList();
            memberMessageDetailBO.InboxCount = memberMessages.Items.Count(msg => msg.IsArchived.HasValue && msg.IsArchived.Value == false || !msg.IsArchived.HasValue);
            memberMessageDetailBO.ArchiveCount = memberMessages.Items.Count(msg => msg.IsArchived.HasValue && msg.IsArchived.Value);
            memberMessageDetailBO.InboxUnreadCount = memberMessages.Items.Count(msg => !msg.IsRead && (msg.IsArchived.HasValue && msg.IsArchived.Value == false || !msg.IsArchived.HasValue));
            memberMessageDetailBO.ArchiveUnreadCount = memberMessages.Items.Count(msg => !msg.IsRead && msg.IsArchived.HasValue && msg.IsArchived.Value);

            return memberMessageDetailBO;
        }
    }
}