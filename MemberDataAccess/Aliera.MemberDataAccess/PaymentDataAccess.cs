using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Microsoft.Extensions.Options;
using Aliera.Utilities;
using Aliera.BusinessObjects.RH;
using Aliera.Utilities.RHIntegration;
using Newtonsoft.Json;
using System.Xml;
using Aliera.BusinessObjects.Broker;
using System.Net.Http;
using System.Text;

namespace Aliera.MemberDataAccess
{
    public class PaymentDataAccess : IPaymentDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IRectangleGatewayService _rectangleGatewayService;

        /// <summary>
        /// constructor for Payment Data Access.
        /// </summary>
        /// <param name="unitOfWork">The unit Of Work.</param>
        public PaymentDataAccess(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings, IRectangleGatewayService rectangleGatewayService)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _rectangleGatewayService = rectangleGatewayService;
        }

        /// <summary>
        /// Gets the payment information.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<PaymentInformationBO> GetPaymentInformation(int userId, AuditLogBO auditLogBO)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
            var memberAddressRepo = _unitOfWork.GetRepository<MemberAddress>();
            var member = await memberRepo.GetFirstOrDefaultAsync(a => a, b => b.UserId == userId);
            var memberDetail = await memberDetailsRepo.GetFirstOrDefaultAsync(a => a, b => b.MemberDetailId == member.MemberDetailId);
            var memberAddress = await memberAddressRepo.GetFirstOrDefaultAsync(a => a, b => b.MemberId == member.MemberId);
            var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a,
              s => s.MemberId == member.MemberId);
            var memberPaymentDetails = await _unitOfWork.GetRepository<MemberPaymentDetail>().GetPagedListAsync(a => a,
                s => s.MemberId == member.MemberId,
              pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            MemberPaymentDetail lastTransactionDetails = null;
            if (memberPaymentDetails.Items.Any())
                lastTransactionDetails = memberPaymentDetails.Items.OrderByDescending(a => a.PaidDate).Take(1).FirstOrDefault();

            var paymentInformationBO = MemberMapper.Map(memberSubscription, memberAddress, memberDetail, lastTransactionDetails);

            //Log audit
            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Update, null);
            return paymentInformationBO;
        }

        /// <summary>
        /// Updates the payment information.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> UpdatePaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO)
        {
            var rows = 0;
            var statusCode = PaymentUpdate.IsNotUpdated;

            if (paymentInformation != null)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, b => b.UserId == paymentInformation.UserId);

                var memberPayInfoToUpdate = await _unitOfWork.GetRepository<MemberSubscription>().GetPagedListAsync(a => a, predicate: b => b.MemberId == member.MemberId,
                     pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                var response = await UpdatePaymentDetailsToAdmin123(paymentInformation, member.Admin123Id, member.MemberId,
                    member.MemberDetailId); //Updating payment information Admin123

                if (response.Trim().ToLower().Equals("ok"))
                {
                    foreach (var memberPaymentInfo in memberPayInfoToUpdate.Items)
                    {
                        if (paymentInformation.IsCardUpdate)
                        {
                            memberPaymentInfo.RoutingNumber = string.Empty;
                            memberPaymentInfo.CardOrAccountNumber = paymentInformation.CreditCardNumber;
                            memberPaymentInfo.PaymentType = (int)PaymentType.CC;
                            memberPaymentInfo.ModifiedBy = paymentInformation.UserId;
                            memberPaymentInfo.ModifiedOn = DateTime.Now;
                        }
                        else
                        {
                            memberPaymentInfo.CardOrAccountNumber = paymentInformation.BankAccountNumber;
                            memberPaymentInfo.PaymentType = (int)PaymentType.ACH;
                            memberPaymentInfo.RoutingNumber = paymentInformation.BankRoutingNumber;
                            memberPaymentInfo.ModifiedBy = paymentInformation.UserId;
                            memberPaymentInfo.ModifiedOn = DateTime.Now;
                        }
                        _unitOfWork.GetRepository<MemberSubscription>().Update(memberPaymentInfo);
                    }
                    rows = _unitOfWork.SaveChanges();
                    if (rows > 0)
                        statusCode = PaymentUpdate.IsUpdated;
                }

                //Log audit for update action on Payment Information
                await AuditMapper.AuditLogging(auditLogBO, paymentInformation.MemberId, AuditAction.Update, paymentInformation);
            }
            return (int)statusCode;
        }

        /// <summary>
        /// Gets the payment receipt details.
        /// </summary>
        /// <param name="paymentReceiptFilterBO">The payment receipt filter bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<MemberPaymentReceiptDetailsBO>> GetPaymentReceiptDetails(PaymentReceiptFilterBO paymentReceiptFilterBO, AuditLogBO auditLogBO)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var memberPaymentDetailsRepo = _unitOfWork.GetRepository<MemberPaymentDetail>();
            var member = await memberRepo.GetFirstOrDefaultAsync(a => a, b => b.UserId == paymentReceiptFilterBO.UserId);
            var memberPayment = await memberPaymentDetailsRepo.GetPagedListAsync(a => a,
                predicate: b => b.MemberId == member.MemberId && b.Status.Trim() == MemberConstants.TransactionStatus,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a,
                predicate: ms => ms.MemberId == member.MemberId);

            var paymentReceiptDetailsBO = MemberMapper.Map(memberPayment, memberSubscription.DocumentId);

            //Sorting logic for receipts
            if (!string.IsNullOrWhiteSpace(paymentReceiptFilterBO.SortColumn))
            {
                if (paymentReceiptFilterBO.StartDate == null)
                    paymentReceiptFilterBO.StartDate = DateTime.MinValue;

                if (paymentReceiptFilterBO.ToDate == null)
                    paymentReceiptFilterBO.ToDate = DateTime.MaxValue;

                switch (Regex.Replace(paymentReceiptFilterBO.SortColumn, @"\s+", ""))
                {
                    case MemberConstants.PaidDate:
                        paymentReceiptDetailsBO = paymentReceiptFilterBO.IsSortByDesc ?
                            paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value)
                            .OrderByDescending(x => x.PaidDate).ToList()
                            : paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value).
                            OrderBy(x => x.PaidDate).ToList();

                        break;

                    case MemberConstants.PaymentMethod:
                        paymentReceiptDetailsBO = paymentReceiptFilterBO.IsSortByDesc ?
                            paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value).
                            OrderByDescending(x => x.PaymentMethod).ToList()
                            : paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value).
                            OrderBy(x => x.PaymentMethod).ToList();

                        break;

                    case MemberConstants.Amount:
                        paymentReceiptDetailsBO = paymentReceiptFilterBO.IsSortByDesc ?
                            paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value)
                            .OrderByDescending(x => x.Amount).ToList()
                            : paymentReceiptDetailsBO.Where(x => x.PaidDate >= paymentReceiptFilterBO.StartDate.Value && x.PaidDate <= paymentReceiptFilterBO.ToDate.Value)
                            .OrderBy(x => x.Amount).ToList();

                        break;

                    default:
                        break;
                }
                var totalRecordsCount = paymentReceiptDetailsBO.Count;
                paymentReceiptDetailsBO.ForEach(p => p.TotalRecordsCount = totalRecordsCount);
                if (paymentReceiptFilterBO.RecordsPerPage > 0)
                {
                    var pageStartCount = (paymentReceiptFilterBO.PageNumber - 1) * paymentReceiptFilterBO.RecordsPerPage;
                    paymentReceiptDetailsBO = paymentReceiptDetailsBO.Skip(pageStartCount).Take(paymentReceiptFilterBO.RecordsPerPage).ToList();
                }
            }

            //Log audit for update action on Payment Information
            //await AuditMapper.AuditLogging(auditLogBO, paymentReceiptFilterBO.UserId, AuditAction.Update, paymentReceiptFilterBO);
            return paymentReceiptDetailsBO;
        }

        public async Task<PaymentReceiptTemplateBO> GetMemberPaymentTemplateDetails(long userId, string transactionId)
        {
            var memberSubscribedPlanPagedList = await _unitOfWork.GetRepository<MemberSubscribedPlan>().GetPagedListAsync(
                 a => a,
                 predicate: m => m.MemberSubscription.Member.UserId == userId,
                 include: x => x.Include(a => a.MemberSubscription)
                                 .ThenInclude(b => b.Member)
                                 .ThenInclude(a => a.MemberDetail)
                                 .ThenInclude(b => b.Member)
                                 .ThenInclude(b => b.MemberPaymentDetail)
                                .Include(a => a.Plan)
                                 .ThenInclude(b => b.Product)
                                .Where(m => m.MemberSubscription.Member.MemberPaymentDetail
                                        .Any(mpd => mpd.ProductId == m.Plan.ProductId
                                                && mpd.TransactionId.Trim() == transactionId.Trim()
                                                && mpd.Status.Trim() == MemberConstants.TransactionStatus))
                                .Include(a => a.MemberSubscription.Member.MemberAddress),
                  pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var member = memberSubscribedPlanPagedList?.Items.Select(a => a.MemberSubscription.Member).Distinct().FirstOrDefault();
            var memberPaymentDetails = member?.MemberPaymentDetail?.Where(m => m.TransactionId.Trim() == transactionId.Trim()).ToList();
            var templateDetails = await _unitOfWork.GetRepository<TemplateDetails>().
                                   GetFirstOrDefaultAsync(i => i, predicate: t => t.ProductId == null);
            return MemberMapper.Map(memberSubscribedPlanPagedList?.Items, member, memberPaymentDetails, templateDetails?.Html);
        }

        //public async Task<bool> UpdateDocumentId(PaymentReceiptTemplateBO paymentReceipt)
        //{
        //    var member = await _unitOfWork.GetRepository<Member>().
        //                        GetFirstOrDefaultAsync(a => a, predicate: b => b.ExternalId == paymentReceipt.ExternalId);

        //    var memberPaymentDetailRepo = await _unitOfWork.GetRepository<MemberPaymentDetail>().
        //                        GetPagedListAsync(a => a, predicate: b => b.MemberId == member.MemberId
        //                                                                  && paymentReceipt.ProductIds.Contains(b.ProductId.GetValueOrDefault())
        //                                                                  && b.PaidDate == Convert.ToDateTime(paymentReceipt.PaidDate),
        //                                                                  pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

        //    foreach (var paymentDetail in memberPaymentDetailRepo.Items)
        //    {
        //        paymentDetail.DocumentId = paymentReceipt.DocumentId;
        //        _unitOfWork.GetRepository<MemberPaymentDetail>().Update(paymentDetail);
        //    }
        //    return await _unitOfWork.SaveChangesAsync() > 0;
        //}

        #region
        /// <summary>
        /// Updates the rh payment information.
        /// </summary>
        /// <param name="paymentInformation">The payment information.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        ///
        //public async Task<RHPaymentResponse> UpdateRHPaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO)
        //{
        //    var rhReponse = new RHPaymentResponse();
        //    var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, b => b.UserId == paymentInformation.UserId);
        //    //fetching existing payment type
        //    var existingPaymentType = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a, b => b.MemberId == member.MemberId);

        //    if (paymentInformation != null)
        //    {
        //        var paymentDetail = MemberMapper.MapRH(paymentInformation, _appSettings, member.MemberId);

        //        var getPaymentDetails = await _rectangleGatewayService.GetPaymentDetails(
        //            paymentDetail.CustomerVaultId, _appSettings);

        //        var vaultInfo = await _rectangleGatewayService.GetPaymentDetails(paymentDetail.CustomerVaultId, _appSettings);
        //        if (vaultInfo?.RHCustomerVault?.Customer != null)
        //        {
        //            var customer = vaultInfo?.RHCustomerVault?.Customer;
        //            // adding payment = cc from UI and creditcard number exist in RH (or)
        //            // adding payment = ACH and achaccount number exist in RH then update the number
        //            // there is no payment method change.
        //            if ((existingPaymentType.PaymentType == (int)PaymentType.CC && !string.IsNullOrEmpty(customer.CreditCardNumber)) ||
        //                (existingPaymentType.PaymentType == (int)PaymentType.ACH && !string.IsNullOrEmpty(customer.ACHAccount)))
        //            {
        //                rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, true);
        //                // should update the vault
        //                //return true;
        //            }
        //            // adding payment = cc from UI and creditcard number not exist in RH (or)
        //            // adding payment = ACH and achaccount number not exist in RH then dlete vault and add new vault
        //            // there is no payment method change.
        //            else if ((existingPaymentType.PaymentType == (int)PaymentType.CC && string.IsNullOrEmpty(customer.ACHAccount)) ||
        //                (existingPaymentType.PaymentType == (int)PaymentType.ACH && string.IsNullOrEmpty(customer.CreditCardNumber)))
        //            {
        //                // implement delete functionality
        //                var deletePaymentDetail = MemberMapper.MapDeleteRH(_appSettings, member.MemberId);
        //                rhReponse = await _rectangleGatewayService.DeletePaymentDetails(deletePaymentDetail, _appSettings);

        //                // add new vault

        //                if (rhReponse.Code.Equals(MemberConstants.RHErrorSuccessResponseCode))
        //                {
        //                    rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, false);
        //                }
        //            }
        //            else
        //            {
        //                rhReponse = await AddPaymentDetails(rhReponse, member, paymentDetail);
        //            }
        //        }
        //        else
        //        {
        //            rhReponse = await AddPaymentDetails(rhReponse, member, paymentDetail);
        //        }
        //    }
        //    return rhReponse;
        //}
        #endregion

        public async Task<RHPaymentResponse> UpdateRHPaymentInformation(PaymentInformationBO paymentInformation, AuditLogBO auditLogBO)
        {
            var rhReponse = new RHPaymentResponse();
            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, b => b.UserId == paymentInformation.UserId);
            //fetching existing payment type
            var existingPaymentType = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a, b => b.MemberId == member.MemberId);

            if (paymentInformation != null)
            {
                var paymentDetail = MemberMapper.MapRH(paymentInformation, _appSettings, member.MemberId, member.ExternalId);
                rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, false);

                if (rhReponse.Code.Equals(MemberConstants.RHErrorResponseCode)
                    && rhReponse.Text.ToLower().Trim().Contains(MemberConstants.DuplicateVaultId))
                {
                    var deletePaymentDetail = MemberMapper.MapDeleteRH(_appSettings, member.MemberId, member.ExternalId);
                    rhReponse = await _rectangleGatewayService.DeletePaymentDetails(deletePaymentDetail, _appSettings);

                    if (rhReponse.Code.Equals(MemberConstants.RHErrorSuccessResponseCode))
                    {
                        rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, false);
                    }
                }
            }
            return rhReponse;
        }

        private async Task<RHPaymentResponse> AddPaymentDetails(RHPaymentResponse rhReponse, Member member, RectangleHealthCustomer paymentDetail)
        {
            rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, false);

            if (rhReponse.Code.Equals(MemberConstants.RHErrorResponseCode)
                && rhReponse.Text.ToLower().Trim().Contains(MemberConstants.DuplicateVaultId))
            {
                var deletePaymentDetail = MemberMapper.MapDeleteRH(_appSettings, member.MemberId, member.ExternalId);
                rhReponse = await _rectangleGatewayService.DeletePaymentDetails(deletePaymentDetail, _appSettings);

                if (rhReponse.Code.Equals(MemberConstants.RHErrorSuccessResponseCode))
                {
                    rhReponse = await _rectangleGatewayService.AddPaymentDetails(paymentDetail, _appSettings, false);
                }
            }

            return rhReponse;
        }

        public async Task<RHCompleteTransactionResponse> CompleteRHPayment(string tokenId)
        {
            var rhReponse = new RHCompleteTransactionResponse();
            if (!string.IsNullOrWhiteSpace(tokenId))
            {
                rhReponse = await _rectangleGatewayService.CompletePaymentAction(tokenId, _appSettings);
            }
            return rhReponse;
        }

        /// <summary>
        /// Updates the payment details to admin123.
        /// </summary>
        /// <param name="paymentInfo">The payment information.</param>
        /// <param name="memberExternalId">The member external identifier.</param>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="memberDetailId">The member detail identifier.</param>
        /// <returns></returns>
        public async Task<string> UpdatePaymentDetailsToAdmin123(PaymentInformationBO paymentInfo, string memberExternalId,
                long memberId, long memberDetailId)
        {
            if (paymentInfo != null && memberExternalId != string.Empty && memberId > 0 && memberDetailId > 0)
            {
                var memberID = Convert.ToString(memberId);
                var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a, b => b.MemberDetailId == memberDetailId);
                var admin123 = new Admin123Mapper(_unitOfWork);
                var admin123BO = new Admin123BO
                {
                    CORPID = _appSettings.Value.CorpId,
                    AGENT = _appSettings.Value.AgentId,
                    LASTNAME = memberDetails.LastName,
                    PAYMENT = new PAYMENT
                    {
                        PAYMENTTYPE = paymentInfo.PaymentMethodType.ToLower().Equals(PaymentType.CC.ToString().ToLower()) ?
                                  PaymentType.CC.ToString() : PaymentType.ACH.ToString(),
                        TOKEN = AppSettings.Environment == Utilities.Enumerations.Environment.Production.ToString() ?
                                                memberID : string.Concat(memberID, "-", AppSettings.Environment)
                    }
                };

                var memberDetailsObject = JsonConvert.SerializeObject(admin123BO);
                var uri = new Uri(_appSettings.Value.Admin123Url);
                var stringContent = new StringContent(memberDetailsObject, UnicodeEncoding.UTF8, "application/json");
                var task = Task.Run(() => admin123.PostURIAsync(uri, stringContent,
                    _appSettings.Value.Admin123User, _appSettings.Value.Admin123Password, memberDetailsObject, memberExternalId));
                task.Wait();

                return task.Result;
            }

            return string.Empty;
        }

        /// <summary>
        /// Validates the credit card.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number.</param>
        /// <param name="paymentMethodType">Type of the payment method.</param>
        /// <returns></returns>
        private static string ValidateCreditCard(string creditCardNumber, string paymentMethodType)
        {
            if (paymentMethodType.ToLower().Equals(PaymentType.CC.ToString().ToLower()) && !string.IsNullOrEmpty(creditCardNumber))
            {
                creditCardNumber = creditCardNumber.Trim();
                if (creditCardNumber.Length == 16 && (creditCardNumber.StartsWith("65") ||
                creditCardNumber.StartsWith("644") || creditCardNumber.StartsWith("6011")))
                {
                    return CCType.Discover.ToString();
                }
                else if (creditCardNumber.Length == 15 && (creditCardNumber.StartsWith("3") ||
                   creditCardNumber.StartsWith("37")))
                {
                    return CCType.AmericanExpress.ToString();
                }
                else if (creditCardNumber.Length == 16 && creditCardNumber.StartsWith("4"))
                {
                    return CCType.Visa.ToString();
                }
                else if (creditCardNumber.Length == 16 && creditCardNumber.StartsWith("5"))
                {
                    return CCType.Mastercard.ToString();
                }
            }

            return string.Empty;
        }
    }
}