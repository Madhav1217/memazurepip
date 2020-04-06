using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.AuthUtility;

namespace Aliera.MemberWorkflow.Controllers
{
    [Authorize]
    [Route("api/member/[Controller]")]
    [ApiController]
    public class MemberMessageController : ControllerBase
    {
        private readonly IMemberMessageService _messageService;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberMessageController(IMemberMessageService messageService,
            IOptions<AppSettings> appSettings,
            IHttpContextAccessor httpContextAccessor)
        {
            _messageService = messageService;
            _httpContextAccessor = httpContextAccessor;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets Member Messages for the last 7 days
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isArchived"></param>
        /// <returns></returns>
        [Route("GetMemberMessages/{userId}/{isArchived}")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanRead")]
        public async Task<JsonResult> GetMemberMessages(long userId, bool isArchived)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var messages = await _messageService.GetMemberMessages(userId, isArchived, auditLogBO);
            return new JsonResult(messages);
        }

        /// <summary>
        /// Method to fetch unread msgs count by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("GetUnreadMessageCount/{userId}")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanRead")]
        public async Task<JsonResult> GetUnreadMessageCount(long userId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var unreadCount = 0;
            if (userId > 0)
                unreadCount = await _messageService.GetUnreadMessageCount(userId, auditLogBO);

            return new JsonResult(unreadCount);
        }

        /// <summary>
        /// Method to mark a message as read by MemberMessageId
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        [Route("MarkAsRead/{messageId}")]
        [HttpGet]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanRead")]
        public async Task<JsonResult> MarkMessageAsRead(long messageId)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);

            var rows = await _messageService.MarkMessageAsRead(messageId, auditLogBO);

            return new JsonResult(rows);
        }

        /// <summary>
        /// Method to delete a message by MemberMessageId
        /// </summary>
        /// <param name="memberMessageIds"></param>
        /// <returns></returns>
        [Route(nameof(DeleteMessage))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanDelete")]
        public async Task<JsonResult> DeleteMessage([FromBody]List<long> memberMessageIds)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            var rows = await _messageService.DeleteMessage(memberMessageIds, auditLogBO);
            return new JsonResult(rows);
        }

        /// <summary>
        /// Archives the messages.
        /// </summary>
        /// <param name="isArchive">if set to <c>true</c> [is archive].</param>
        /// <param name="memberMessageIds">The member message ids.</param>
        /// <returns></returns>
        [Route("ArchiveMessage/{isArchive}")]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanUpdate")]
        public async Task<JsonResult> ArchiveMessages(bool isArchive, [FromBody]List<long> memberMessageIds)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);

            var rows = await _messageService.MarkMessageAsArchived(memberMessageIds, isArchive, auditLogBO);

            return new JsonResult(rows);
        }

        /// <summary>
        /// Gets the member messages by page.
        /// </summary>
        /// <param name="memberMessageDetailBO">The member message detail bo.</param>
        /// <returns></returns>
        [Route(nameof(GetMemberMessagesByPage))]
        [HttpPost]
        [ClaimRequirement("roles", "MEM_VIEW_MESSAGES", "CanRead")]
        public async Task<JsonResult> GetMemberMessagesByPage([FromBody]MessageDetailBO memberMessageDetailBO)
        {
            var jwt = await HttpContext.GetTokenAsync(BrokerConstants.TokenScheme, BrokerConstants.AccessToken);
            var auditLogBO = new AuditLogBO(_appSettings.Value.ApplicationName, jwt, _httpContextAccessor);
            MessageDetailBO messages = null;
            messages = await _messageService.GetMemberMessagesByPage(memberMessageDetailBO, auditLogBO);

            return new JsonResult(messages);
        }
    }
}