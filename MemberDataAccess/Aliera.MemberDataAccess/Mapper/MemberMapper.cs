using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Notifications;
using Aliera.Utilities.RHIntegration;
using System.Text;
using System.Text.RegularExpressions;

namespace Aliera.MemberDataAccess.Mapper
{
    public static class MemberMapper
    {
        public static SMS Map(string modeValue, string messageBody, IOptions<AppSettings> smsSettings)
        {
            return new SMS
            {
                Number = modeValue,
                Text = messageBody,
                AccountSid = smsSettings.Value.AccountSid,
                AuthToken = smsSettings.Value.AuthToken,
                PhoneNumber = smsSettings.Value.PhoneNumber
            };
        }

        public static Email Map(string modeValue, string messageBody, string subject, IOptions<AppSettings> emailSettings, string fromEmail)
        {
            return new Email
            {
                SmtpServer = emailSettings.Value.SmtpServer,
                PortNumber = emailSettings.Value.PortNumber,
                UserEmail = emailSettings.Value.UserEmail,
                Password = emailSettings.Value.Password,
                ToMailId = modeValue,
                FromMailId = !string.IsNullOrEmpty(fromEmail) ? fromEmail : emailSettings.Value.UserEmail,
                Content = messageBody,
                MailSubject = subject
            };
        }

        #region DOCUMENTANDFORM

        public static List<DocumentAndFormBO> Map(IPagedList<DocumentAndForm> documentandForms)
        {
            return documentandForms.Items.Select(df => new DocumentAndFormBO
            {
                DocumentAndFormId = df.DocumentAndFormId,
                CategoryName = df.DocumentCategory?.DocumentCategoryName,
                DocumentName = df.DocumentName,
                Ext = df.Ext,
                LastUpdatedOn = df.LastUpdatedOn,
                URL = df.Path,
                PortalId = df.PortalId,
                DocumentId = df.DocumentId.HasValue ? df.DocumentId.Value : 0
            }).ToList();
        }

        //public static List<DocumentAndForm> Map(IPagedList<DocumentAndFormBO> documentandFormBOs)
        //{
        //    return documentandFormBOs.Items.Select(df => new DocumentAndForm
        //    {
        //        DocumentAndFormId = df.DocumentAndFormId,
        //        CategoryName = df.SectionDisplayName,
        //        DocumentName = df.DocumentName,
        //        Ext = df.Ext,
        //        LastUpdatedOn = df.LastUpdatedOn,
        //        Path = df.URL,
        //        DocumentId = df.DocumentId
        //    }).ToList();
        //}

        #endregion DOCUMENTANDFORM

        public static List<MemberFaqCategoryBO> Map(IPagedList<Faqcategory> memberFaqcatetory)
        {
            return memberFaqcatetory.Items.Select(fc => new MemberFaqCategoryBO
            {
                CatgId = fc.FaqcategoryId,
                Category = fc.Category
            }).ToList();
        }

        public static List<MemberFaqBO> Map(IPagedList<Faq> memberFaq)
        {
            return memberFaq.Items.Select(faq => new MemberFaqBO
            {
                Faqid = faq.Faqid,
                FaqcatgId = faq.FaqcategoryId,
                Faqquestion = faq.Faqquestion,
                Faqanswer = faq.Faqanswer
            }).ToList();
        }

        public static MemberFeedback Map(MemberFeedbackBO memberFeedbackBO)
        {
            return new MemberFeedback
            {
                MemberId = memberFeedbackBO.MemberExternalId,
                Email = memberFeedbackBO.Email,
                Message = memberFeedbackBO.Message,
                SourceId = memberFeedbackBO.SourceId == 1 ? Convert.ToInt32(MemberFeedbackType.ShareStory) : Convert.ToInt32(MemberFeedbackType.ShareContactInfo),
                CreatedBy = memberFeedbackBO.UserId,
                PhoneNumber = memberFeedbackBO.PhoneNumber,
                InquiryType = memberFeedbackBO.InquiryType,
                InquiryTypeEmail = memberFeedbackBO.InquiryTypeEmail,
                CreatedOn = DateTime.UtcNow
            };
        }

        public static MemberDataBO Map(Member member, MemberDetail memberDetail)
        {
            return new MemberDataBO
            {
                UserId = member.UserId.Value,
                ExternalId = member.ExternalId,
                FirstName = memberDetail.FirstName,
                LastName = memberDetail.LastName,
                FullName = memberDetail.FirstName + " " + memberDetail.LastName,
                MemberType = MemberConstants.Primary
            };
        }

        public static MemberDemographicsBO Map(Member member, MemberAddress memberAddress, MemberDetail memberDetail, IList<MemberSubscription> memberSubscription)
        {
            return new MemberDemographicsBO
            {
                MemberID = member.MemberId,
                ExternalMemberId = member.ExternalId,
                Address1 = memberAddress.AddressLine1,
                Address2 = memberAddress.AddressLine2,
                FirstName = memberDetail.FirstName,
                LastName = memberDetail.LastName,
                DateofBirth = memberDetail.DateOfBirth,
                Gender = memberDetail.Gender,
                City = memberAddress.City,
                StateProvince = memberAddress.StateCode,
                ZipCode = memberAddress.ZipCode,
                Email = memberDetail.EmailId,
                PhoneNumber = memberDetail.PhoneNumber,
                MemberType = MemberConstants.Primary,
                MemberDetailId = member.MemberDetailId,
                PaidThroughDate = memberSubscription.Select(j => j.EndDate).FirstOrDefault(),
                GroupID = memberSubscription.Select(j => j.GroupId).FirstOrDefault(),
                DivisionID = memberSubscription.Select(j => j.DivisionId).FirstOrDefault()
            };
        }

        public static ServiceInformationBO Map(Member member, MemberDetail memberDetail, List<string> planNames,
            List<string> productnames, List<ProductInformationBO> products, int levelOfCoverage, DateTime? paidThroughDate)
        {
            return new ServiceInformationBO
            {
                DependentDetailId = member.MemberDetailId,
                FirstName = memberDetail.FirstName,
                LastName = memberDetail.LastName,
                MemberName = memberDetail.FirstName + memberDetail.LastName,
                MemberType = MemberConstants.Primary,
                PlanName = planNames,
                Product = productnames,
                ProductInformation = products,
                LevelOfCoverage = ((FamilyIndicator)levelOfCoverage).ToString(),
                PaidThroughDate = paidThroughDate,
                DependentStatus = DependentStatus.Active.ToString()
            };
        }

        public static PaymentInformationBO Map(MemberSubscription memberSubscription, MemberAddress memberAddress,
            MemberDetail memberDetail, MemberPaymentDetail paymentDetail)
        {
            return new PaymentInformationBO
            {
                PaymentMethodType = memberSubscription != null && memberSubscription.PaymentType != null ?
                                    ((PaymentType)memberSubscription.PaymentType).ToString() : null,
                Last4Digits = memberSubscription.CardOrAccountNumber,
                LastTransactionDate = paymentDetail != null
                    ? (paymentDetail.PaidDate != null ? paymentDetail.PaidDate.ToString() : "")
                    : "",
                PremiumAmount = Convert.ToDecimal(paymentDetail != null ? paymentDetail.PaidAmount : 0),
                //Field for auto populate
                FirstName = memberDetail.FirstName,
                LastName = memberDetail.LastName,
                Address = memberAddress.AddressLine1 + (memberAddress.AddressLine2 ?? ""),
                City = memberAddress.City,
                State = memberAddress.StateCode,
                ZipCode = memberAddress.ZipCode
            };
        }

        public static DeleteRectangleHealthCustomer MapDeleteRH(IOptions<AppSettings> appSettings, long memberId, string externalId)
        {
            var memberID = Convert.ToString(memberId);
            return new DeleteRectangleHealthCustomer
            {
                APIKey = appSettings.Value.RHAPIKey,
                //CustomerVaultId = AppSettings.Environment == Utilities.Enumerations.Environment.Production.ToString() ?
                //                                    memberID : string.Concat(memberID, "-", AppSettings.Environment)
                CustomerVaultId = externalId
            };
        }

        public static List<SecurityQuestionAnswersBO> Map(IPagedList<UserSecurityQuestionAnswers> userSecurityQAs)
        {
            return userSecurityQAs.Items.Select(item => new SecurityQuestionAnswersBO
            {
                SecurityQuestionId = item.SecurityQuestionId,
                Answer = item.Answer,
                UserId = item.UserId
            }).ToList();
        }

        public static MemberDetail Map(DependentDetailsBO dependentMember, MemberDetail detail)
        {
            return new MemberDetail
            {
                FirstName = dependentMember.FirstName.Trim(),
                LastName = dependentMember.LastName.Trim(),
                EmailId = detail.EmailId,
                PhoneNumber = detail.PhoneNumber,
                Gender = dependentMember.Gender,
                DateOfBirth = Convert.ToDateTime(dependentMember.DOB),
                CreatedBy = dependentMember.MemberId,
                CreatedOn = DateTime.Now,
                Ssn = dependentMember.Ssn ?? string.Empty
            };
        }

        public static MemberDependent Map(DependentDetailsBO dependentMember, long memberDetailId)
        {
            return new MemberDependent
            {
                MemberId = dependentMember.MemberId,
                MemberDetailId = memberDetailId,
                IsTobaccoUser = dependentMember.isTobaccoUser,
                CreatedOn = DateTime.Now,
                CreatedBy = dependentMember.MemberId,
                RelationshipId = dependentMember.RelationshipId
            };
        }

        public static MemberDependentAddress Map(DependentDetailsBO dependentMember, long memberDetailId, MemberAddress memberAddress)
        {
            return new MemberDependentAddress
            {
                MemberId = dependentMember.MemberId,
                MemberDetailId = memberDetailId,
                AddressTypeId = memberAddress.AddressTypeId,
                AddressLine1 = memberAddress.AddressLine1,
                AddressLine2 = memberAddress.AddressLine2,
                City = memberAddress.City,
                ZipCode = memberAddress.ZipCode.Trim(),
                StateCode = memberAddress.StateCode,
                CreatedOn = DateTime.Now,
                CreatedBy = memberAddress.MemberId
            };
        }

        public static MemberAddress Map(MemberAddressBO memberAddressBO)
        {
            return new MemberAddress
            {
                AddressLine1 = memberAddressBO.AddressLine1,
                AddressLine2 = memberAddressBO.AddressLine2,
                City = memberAddressBO.City,
                StateCode = memberAddressBO.StateCode,
                ZipCode = memberAddressBO.ZipCode
            };
        }

        public static Users Map(Users user, string guid, long userId)
        {
            //user.ResetPasswordAccessCode = guid;
            //user.ResetPasswordAccessCodeDateTime = DateTime.UtcNow;
            user.ModifiedBy = userId;
            user.ModifiedOn = DateTime.UtcNow;
            user.IsEmailVerified = false;
            return user;
        }

        public static List<MemberPaymentReceiptDetailsBO> Map(IPagedList<MemberPaymentDetail> memberPaymentDetail, long? receiptId)
        {
            return memberPaymentDetail.Items.GroupBy(item => item.TransactionId).Select(item => new MemberPaymentReceiptDetailsBO
            {
                TransactionId = item.FirstOrDefault().TransactionId,
                PaidDate = item.FirstOrDefault().PaidDate,
                PaymentMethod = item.FirstOrDefault().Method,
                Amount = item.Sum(s => s.PaidAmount),
                Status = item.FirstOrDefault().Status
            }).ToList();
        }

        public static List<MemberQuestionnaireBO> Map(IList<MemberQuestionnaire> questionnaires)
        {
            var dependentQuestionnaireBOs = new List<MemberQuestionnaireBO>();
            foreach (var questionnaire in questionnaires)
            {
                var memberQuestionnaireBO = new MemberQuestionnaireBO
                {
                    MemberQuestionnaireId = questionnaire.MemberQuestionnaireId,
                    MemberQuestionnaireType = questionnaire.MemberQuestionnaireType,
                    MemberQuestionDependentId = questionnaire.MemberQuestionDependentId,
                    MemberQuestionnaire = questionnaire.MemberQuestionnaire1,
                    MemberQuestionAnswer = questionnaire.MemberQuestionAnswer
                };
                dependentQuestionnaireBOs.Add(memberQuestionnaireBO);
            }
            return dependentQuestionnaireBOs;
        }

        public static PaymentReceiptTemplateBO Map(IEnumerable<MemberSubscribedPlan> memberSubscribedPlans, Member member,
                                                    IEnumerable<MemberPaymentDetail> memberPaymentDetails, string html)
        {
            var memberAddress = member.MemberAddress?.FirstOrDefault(a => a.AddressTypeId == (int)AddressType.Primary);

            var paymentReiptTemplateBO = new PaymentReceiptTemplateBO()
            {
                ExternalId = member.ExternalId,
                FullName = $"{member.MemberDetail.FirstName} {member.MemberDetail.LastName}",
                Address = $"{memberAddress?.AddressLine1} {memberAddress?.AddressLine2}",
                City = memberAddress?.City,
                State = memberAddress?.StateCode,
                Zip = memberAddress?.ZipCode,
                Phone = GetFormattedPhoneNumber(member.MemberDetail.PhoneNumber),
                Email = member.MemberDetail.EmailId,
                Type = MemberConstants.Type,
                PaymentNumber = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault().PaymentNumber?.ToString(),
                PaidDateTitle = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.PaidDate?.ToString("MMMM dd,yyyy"),
                PaidDate = string.Format("{0} at {1}", memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.PaidDate?.ToString("MM/dd/yyyy"), memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.PaidDate?.ToString("t")),
                PayDate = $"{memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.PaidDate?.ToString("MMMM dd,yyyy")} at {memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.PaidDate?.ToString("t")}",
                Status = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.Status,
                TransactionStatus = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.Status,
                OrderId = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.InvoiceId,
                TransactionId = memberPaymentDetails?.OrderByDescending(a => a.TransactionId).FirstOrDefault()?.TransactionId,
                CardNumber = member.MemberSubscription.FirstOrDefault().CardOrAccountNumber,
                Html = html
            };

            var method = memberPaymentDetails?.OrderByDescending(a => a.PaidDate).FirstOrDefault()?.Method;
            paymentReiptTemplateBO.CardType = method == "ACH" ? "ACH" :
                                                Enum.GetName(typeof(CCType), member.MemberSubscription.FirstOrDefault().CardType.GetValueOrDefault());
            var builder = new StringBuilder();
            foreach (var subscribedPlan in memberSubscribedPlans)
            {
                builder.Append($"<tr><td style=\"padding: 3px;vertical-align: top;\">{subscribedPlan.Plan.Product.Name}<br> {Enum.GetName(typeof(FamilyIndicator), subscribedPlan.FamilyIndicator)} - ID: {subscribedPlan.Plan.Product.ProductCode} - Payment: {paymentReiptTemplateBO.PaymentNumber} <br> </td></tr>");
            }
            paymentReiptTemplateBO.Products = builder.ToString();
            decimal amountTotal = 0;
            foreach (var mpd in memberPaymentDetails)
            {
                amountTotal += mpd.PaidAmount.GetValueOrDefault();
            }
            paymentReiptTemplateBO.TotalAmount = amountTotal.ToString("C");
            return paymentReiptTemplateBO;
        }

        public static IDCardBO Map(IEnumerable<MemberSubscribedPlan> memberSubscribedPlans,
            Member member, string dependents, string htmlTemplate, int productCode)
        {
            var iDdata = new IDCardBO();
            decimal msraDec = 0;
            var planType = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                    .Select(msp => msp.Plan.Type).FirstOrDefault();
            var msra = memberSubscribedPlans.Where(x => x.Plan.Msra != null && x.Plan.Product.ProductCode == productCode)
                            .Select(a => a.Plan.Msra).FirstOrDefault();
            if (!string.IsNullOrEmpty(msra))
                msraDec = Convert.ToDecimal(msra);
            iDdata.CQProgramMSRA = (msra != null) ? msraDec.ToString("C0") : string.Empty;
            iDdata.ActiveDependentName = dependents != null ? dependents : "No Dependents";
            iDdata.FullName = $"01 {member.MemberDetail.FirstName} {member.MemberDetail.LastName}";
            iDdata.ExternalId = member.Admin123Id;
            var effectiveDate = memberSubscribedPlans.FirstOrDefault()?.MemberSubscription?.EffectiveDate;
            iDdata.EffectiveShort = effectiveDate.HasValue ? effectiveDate.Value.ToString("MM/dd/yyyy") : string.Empty;

            if (planType == (int)PlanType.Primary)
            {
                var Products = memberSubscribedPlans.Where(msp => msp.Plan.Type == (int)PlanType.Primary)
                                                        .Select(msp => msp.Plan.Product.Name);
                var outerHTML = "<table style=\"padding-left: 58px;margin-top: -15px;font-size: 10px;\"><tbody>{0}</tbody></table>";
                var builder = new StringBuilder();
                foreach (var product in Products)
                {
                    builder.Append($"<tr><td style=\"line-height: 8px;\">&nbsp;{product}</td></tr>");
                }
                iDdata.ProductAdminlabel = string.Format(outerHTML, builder.ToString());
            }
            else
            {
                iDdata.ProductAdminlabel = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                            .Select(msp => msp.Plan.Product.Name).FirstOrDefault();
            }
            iDdata.Html = htmlTemplate;
            iDdata.PCP = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                    .Select(msp => msp.Plan.Product.Pcp).FirstOrDefault();
            iDdata.UrgentCare = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                   .Select(msp => msp.Plan.Product.UrgentCare).FirstOrDefault();
            iDdata.Speciality = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                   .Select(msp => msp.Plan.Product.Speciality).FirstOrDefault();
            iDdata.ER = memberSubscribedPlans.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                   .Select(msp => msp.Plan.Product.Er).FirstOrDefault();

            switch (productCode)
            {
                case (int)Supplemental.UnityDental:
                case (int)Supplemental.UnityVision:
                case (int)Supplemental.UnityDentalVision:
                    if (memberSubscribedPlans.Count() == 1 && planType == (int)PlanType.Supplemental)
                        iDdata.Group = MemberConstants.UnitySingle;
                    else
                        iDdata.Group = MemberConstants.UnityGroup;
                    break;

                case (int)Supplemental.TrinityDental:
                case (int)Supplemental.TrinityVision:
                case (int)Supplemental.TrinityDentalVision:
                    if (memberSubscribedPlans.Count() == 1 && planType == (int)PlanType.Supplemental)
                        iDdata.Group = MemberConstants.TrinitySingle;
                    else
                        iDdata.Group = MemberConstants.TrinityGroup;
                    break;

                default:
                    break;
            }
            return iDdata;
        }

        public static RectangleHealthCustomer MapRH(PaymentInformationBO paymentInformationBO, IOptions<AppSettings> appSettings, long memberId, string externalId)
        {
            var memberID = Convert.ToString(memberId);
            return new RectangleHealthCustomer
            {
                APIKey = appSettings.Value.RHAPIKey,

                //CustomerVaultId = AppSettings.Environment == Utilities.Enumerations.Environment.Production.ToString() ?
                //                                    memberID : string.Concat(memberID, "-", AppSettings.Environment),
                CustomerVaultId = externalId,
                RedirectURL = appSettings.Value.RedirectURLForRH,

                Billing = new RectangleBillingInfo
                {
                    FirstName = paymentInformationBO.FirstName,
                    LastName = paymentInformationBO.LastName,
                    Address1 = paymentInformationBO.Address,
                    City = paymentInformationBO.City,
                    State = paymentInformationBO.State,
                    Postal = paymentInformationBO.ZipCode
                }
            };
        }

        public static Users Map(MemberRegistrationBO memberRegistrationBO)
        {
            return new Users
            {
                UserId = memberRegistrationBO.UserId,
                UserType = EntityType.Member,
                UserName = memberRegistrationBO.UserName,
                Email = memberRegistrationBO.Email,
                PortalId = 2,
                IsEmailVerified = memberRegistrationBO.EmailConfirmed,
                LockoutEnabled = true,
                FirstName = memberRegistrationBO.FirstName,
                LastName = memberRegistrationBO.LastName,
                Status = UserStatus.Active,
                PhoneNumber = memberRegistrationBO.PhoneNumber,
                CreatedBy = memberRegistrationBO.UserId,
                CreatedOn = DateTime.UtcNow,
            };
        }

        private static string MaskCard(string input)
        {
            var maskedCardNumberWithSpaces = string.Empty;
            if (!string.IsNullOrEmpty(input))
            {
                var firstDigits = input.Substring(0, input.Length - 4);
                var sb = new StringBuilder();
                foreach (var digit in firstDigits)
                {
                    sb.Append('x');
                }
                var lastDigits = input.Substring(input.Length - 4, 4);
                return string.Concat(sb.ToString(), lastDigits);
            }
            return maskedCardNumberWithSpaces;
        }

        public static string GetFormattedPhoneNumber(string phone)
        {
            if (phone != null && phone.Trim().Length == 10)
                return string.Format("{0}-{1}-{2}", phone.Substring(0, 3), phone.Substring(3, 3), phone.Substring(6, 4));
            return phone;
        }
    }
}