using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.Utilities.RHIntegration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aliera.MemberService
{
    public interface IPaymentService
    {
        Task<PaymentInformationBO> GetPaymentInformation(int userId, AuditLogBO auditLogBO);

        Task<int> UpdatePaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO);

        Task<IEnumerable<MemberPaymentReceiptDetailsBO>> GetPaymentReceiptDetails(PaymentReceiptFilterBO paymentReceiptFilterBO, AuditLogBO auditLogBO);
        Task<byte[]> GetMemberPaymentTemplateDetails(long userId, string transactionId);
        //Task<bool> UpdateDocumentId(PaymentReceiptTemplateBO paymentReceiptTemplate);
        Task<RHPaymentResponse> UpdateRHPaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO);

        Task<RHCompleteTransactionResponse> CompleteRHPayment(string tokenId);
    }
}