using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aliera.Utilities.RHIntegration
{
    public interface IRectangleGatewayService
    {
        /// <summary>
        /// Add customer payemnts details to rectangle health.
        /// </summary>
        Task<RHPaymentResponse> AddPaymentDetails(RectangleHealthCustomer rectangleHealthCustomer, IOptions<AppSettings> appSettings, bool isUpdate);

        /// <summary>
        /// Complete payment action using tokenid and application in rectangle health
        /// </summary>
        Task<RHCompleteTransactionResponse> CompletePaymentAction(string tokenId, IOptions<AppSettings> appSettings);

        /// <summary>
        /// DeletePaymentDetails
        /// </summary>
        /// <param name="deleteRectangleHealthCustomer"></param>
        /// <param name="appSettings"></param>
        /// <param name="isUpdate"></param>
        /// <returns></returns>
        Task<RHPaymentResponse> DeletePaymentDetails(DeleteRectangleHealthCustomer deleteRectangleHealthCustomer, IOptions<AppSettings> appSettings);

        /// <summary>
        /// Gets vault information for requested customer
        /// </summary>
        /// <param name="vaultId"></param>
        /// <param name="appSettings"></param>
        /// <returns></returns>
        Task<RHVaultResponse> GetPaymentDetails(string vaultId, IOptions<AppSettings> appSettings);
    }
}
