using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;

namespace Aliera.MemberService
{
    public class MasterService : IMasterService
    {
        private readonly IMasterDataAccess _masterDa;

        public MasterService(IMasterDataAccess masterDa)
        {
            _masterDa = masterDa;
        }

        /// <summary>
        /// Gets the security questions.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberSecurityQuestionsEmptyErrorCode</exception>
        public async Task<IEnumerable<SecurityQuestionsBO>> GetSecurityQuestions(AuditLogBO auditLogBO)
        {
            var response = await _masterDa.GetSecurityQuestions(auditLogBO);
            if (response == null) throw new CustomException(nameof(MemberConstants.MemberSecurityQuestionsEmptyErrorCode));
            return response;
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MemberStatesEmptyErrorCode</exception>
        public async Task<IEnumerable<StateBO>> GetStates(AuditLogBO auditLogBO)
        {
            var states = await _masterDa.GetStates(auditLogBO);
            if (states == null) throw new CustomException(nameof(MemberConstants.MemberStatesEmptyErrorCode));
            return states;
        }

        /// <summary>
        /// Updates the digital document identifier.
        /// </summary>
        /// <param name="digitalDoc">The digital document.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">MasterServiceUpdateDigitalDocumentIdInputEmptyErrorCode</exception>
        public async Task<bool> UpdateDigitalDocumentId(DigitalDocumentBO digitalDoc, AuditLogBO auditLogBO)
        {
            if (digitalDoc == null)
                throw new CustomException(nameof(MemberConstants.MasterServiceUpdateDigitalDocumentIdInputEmptyErrorCode));
            return await _masterDa.UpdateDigitalDocumentId(digitalDoc, auditLogBO);
        }
    }
}