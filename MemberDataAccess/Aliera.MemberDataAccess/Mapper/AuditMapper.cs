using Aliera.BusinessObjects.Audit;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Aliera.Utilities.AuditLog;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.ExtensionMethods;

namespace Aliera.MemberDataAccess.Mapper
{
    public static class AuditMapper
    {
        /// <summary>
        /// Maps the audit and queue.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <param name="auditAction">The audit action.</param>
        /// <param name="newDataModel">The new data model.</param>
        /// <param name="oldDataModel">The old data model.</param>
        /// <param name="countOfItems">The count of items.</param>
        /// <returns></returns>
        public static async Task MapAuditAndQueue(AuditLogBO auditLogBO, AuditAction auditAction, object newDataModel, object oldDataModel = null, int? countOfItems = null)
        {
            auditLogBO.Action = auditAction;
            auditLogBO.AuditDate = DateTime.UtcNow;
            auditLogBO.NewValue = JsonConvert.SerializeObject(newDataModel);

            if (oldDataModel != null)
            {
                auditLogBO.OldValue = JsonConvert.SerializeObject(oldDataModel);
            }

            if (string.IsNullOrEmpty(auditLogBO.Comments) && newDataModel != null)
            {
                Type type;
                if (newDataModel.GetType().IsGenericType)
                {
                    type = newDataModel.GetType().GetGenericArguments()[0];
                    auditLogBO.Comments = $"{Enum.GetName(typeof(AuditAction), auditAction)} action on a list of {type.Name}. {countOfItems} items have been selected.";
                }
                else
                {
                    type = newDataModel.GetType();
                    auditLogBO.Comments = $"{Enum.GetName(typeof(AuditAction), auditAction)} action on {type.Name}";
                }
            }

            var clonedAuditBO = new AuditLogBO();
            clonedAuditBO = clonedAuditBO.Clone(auditLogBO);

            await AuditLogHelper.QueueAudits(clonedAuditBO);
        }

        /// <summary>
        /// Audits the logging.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <param name="entityTypeId">The entity type identifier.</param>
        /// <param name="auditAction">The audit action.</param>
        /// <param name="newDataModel">The new data model.</param>
        /// <param name="oldDataModel">The old data model.</param>
        /// <param name="entityType">Type of the entity.</param>
        /// <param name="countOfItems">The count of items.</param>
        /// <returns></returns>
        public static async Task AuditLogging(AuditLogBO auditLogBO, long? entityTypeId, AuditAction auditAction,
            object newDataModel, object oldDataModel = null, string entityType = null, int? countOfItems = null)
        {
            auditLogBO.EntityType = entityType ?? Enum.GetName(typeof(EntityType), EntityType.Member);
            auditLogBO.EntityTypeId = entityTypeId;
            await MapAuditAndQueue(auditLogBO, auditAction, newDataModel, oldDataModel, countOfItems);
        }
    }
}