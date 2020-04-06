using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.MemberDataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Logging.CustomExceptions;
using Aliera.Utilities.RHIntegration;
using Microsoft.Extensions.Options;
using Aliera.Utilities.Helpers;
using DinkToPdf.Contracts;

namespace Aliera.MemberService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentDataAccess _paymentDataAccess;
        private IDocConverter _iDocconverter;
        private IConverter _iConverter;

        public PaymentService(IPaymentDataAccess paymentDataAccess, IDocConverter idocConverter, IConverter iConverter)
        {
            _paymentDataAccess = paymentDataAccess;
            _iDocconverter = idocConverter;
            _iConverter = iConverter;
        }

        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">PaymentServiceGetPaymentInformationInputEmptyErrorCode</exception>
        public Task<PaymentInformationBO> GetPaymentInformation(int userId, AuditLogBO auditLogBO)
        {
            if (userId <= 0)
                throw new CustomException(nameof(MemberConstants.PaymentServiceGetPaymentInformationInputEmptyErrorCode));
            return _paymentDataAccess.GetPaymentInformation(userId, auditLogBO);
        }

        /// <summary>
        /// Updates the payment information.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">PaymentServiceUpdatePaymentInformationInputEmptyErrorCode</exception>
        public Task<int> UpdatePaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO)
        {
            if (paymentInformation == null)
                throw new CustomException(nameof(MemberConstants.PaymentServiceUpdatePaymentInformationInputEmptyErrorCode));
            return _paymentDataAccess.UpdatePaymentInformation(paymentInformation, auditLogBO);
        }

        /// <summary>
        /// Gets the payment receipt details.
        /// </summary>
        /// <param name="paymentReceiptFilterBO">The payment receipt filter bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">PaymentServiceGetPaymentReceiptDetailsInputEmptyErrorCode</exception>
        public Task<IEnumerable<MemberPaymentReceiptDetailsBO>> GetPaymentReceiptDetails(PaymentReceiptFilterBO paymentReceiptFilterBO, AuditLogBO auditLogBO)
        {
            if (paymentReceiptFilterBO == null)
                throw new CustomException(nameof(MemberConstants.PaymentServiceGetPaymentReceiptDetailsInputEmptyErrorCode));
            return _paymentDataAccess.GetPaymentReceiptDetails(paymentReceiptFilterBO, auditLogBO);
        }

        public async Task<byte[]> GetMemberPaymentTemplateDetails(long userId, string transactionId)
        {
            var paymentTemplateBO = await _paymentDataAccess.GetMemberPaymentTemplateDetails(userId, transactionId);
            var html = Utilities.UtilityHelper.ReplaceParametersWithValues(paymentTemplateBO.GetType(), paymentTemplateBO, paymentTemplateBO.Html);
            return _iDocconverter.ConvertHtmlToPDF(_iConverter, html);
        }
        //public async Task<bool> UpdateDocumentId(PaymentReceiptTemplateBO paymentReceiptTemplate)
        //{
        //   return await _paymentDataAccess.UpdateDocumentId(paymentReceiptTemplate);
        //}

        /// <summary>
        /// Updates the payment information.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// /// <param name="auditLogBO">The payment information.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">PaymentServiceUpdatePaymentInformationInputEmptyErrorCode</exception>
        public Task<RHPaymentResponse> UpdateRHPaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO)
        {
            if (paymentInformation == null)
                throw new CustomException(nameof(MemberConstants.PaymentServiceUpdatePaymentInformationInputEmptyErrorCode));
            return _paymentDataAccess.UpdateRHPaymentInformation(paymentInformation, auditLogBO);
        }

        public Task<RHCompleteTransactionResponse> CompleteRHPayment(string tokenId)
        {
            if (string.IsNullOrWhiteSpace(tokenId))
                throw new CustomException(nameof(MemberConstants.PaymentServiceUpdatePaymentInformationInputEmptyErrorCode));
            return _paymentDataAccess.CompleteRHPayment(tokenId);
        }
    }
}