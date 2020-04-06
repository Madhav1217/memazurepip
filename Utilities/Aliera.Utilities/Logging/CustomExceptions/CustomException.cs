using System;
using Aliera.Utilities.Constants;

namespace Aliera.Utilities.Logging.CustomExceptions
{
    public class CustomException : Exception
    {
        public int ErrorCode { get; set; }
        public new string InnerException { get; set; }
        public string ErrorDescription { get; set; }
        public new string StackTrace { get; set; }
        public string RequestMethod { get; set; }
        public string MethodName { get; set; }
        public string FileName { get; set; }
        public string LoggedInUser { get; set; }

        public CustomException()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception exception) : base(message, exception)
        {
        }

        public double CustomExceptionCode(string message)
        {
            double errorcode = 0;
            var metadata = "";
            if (message.Contains('.'))
            {
                int lastIndex = message.LastIndexOf('.');
                metadata = message.Substring(lastIndex + 1);
                message = message.Substring(0, lastIndex);
            }
            switch (message)
            {
                case nameof(BrokerConstants.UserNotFoundErrorCode):
                    errorcode = BrokerConstants.UserNotFoundErrorCode;
                    break;

                case nameof(BrokerConstants.ModelValidationErrorCode):
                    errorcode = BrokerConstants.ModelValidationErrorCode;
                    break;

                case nameof(BrokerConstants.NoSecurityQuestionsErrorCode):
                    errorcode = BrokerConstants.NoSecurityQuestionsErrorCode;
                    break;

                case nameof(BrokerConstants.InvalidPortalId):
                    errorcode = BrokerConstants.InvalidPortalId;
                    break;

                case nameof(BrokerConstants.AccountLocked):
                    errorcode = BrokerConstants.AccountLocked;
                    break;

                case nameof(BrokerConstants.PasswordExpired):
                    errorcode = BrokerConstants.PasswordExpired;
                    break;

                case nameof(BrokerConstants.LastAttemptLeftErrorCode):
                    errorcode = BrokerConstants.LastAttemptLeftErrorCode;
                    break;

                case nameof(BrokerConstants.ThirdAttemptFailedErrorCode):
                    errorcode = BrokerConstants.ThirdAttemptFailedErrorCode;
                    break;

                case nameof(BrokerConstants.SecondAttemptFailedErrorCode):
                    errorcode = BrokerConstants.SecondAttemptFailedErrorCode;
                    break;

                case nameof(BrokerConstants.FirstAttemptFailedErrorCode):
                    errorcode = BrokerConstants.FirstAttemptFailedErrorCode;
                    break;

                case nameof(BrokerConstants.BadRequest):
                    errorcode = BrokerConstants.BadRequest;
                    break;

                case nameof(BrokerConstants.PasswordAlreadyExists):
                    errorcode = BrokerConstants.PasswordAlreadyExists;
                    break;

                case nameof(BrokerConstants.PasswordShouldNotSameAsUserName):
                    errorcode = BrokerConstants.PasswordShouldNotSameAsUserName;
                    break;

                case nameof(BrokerConstants.InvalidCurrentPassword):
                    errorcode = BrokerConstants.InvalidCurrentPassword;
                    break;

                case nameof(BrokerConstants.AnswersDoNotMatchErrorCode):
                    errorcode = BrokerConstants.AnswersDoNotMatchErrorCode;
                    break;

                case nameof(BrokerConstants.InvalidPasswordResetTokenErrorCode):
                    errorcode = BrokerConstants.InvalidPasswordResetTokenErrorCode;
                    break;

                case nameof(BrokerConstants.AccountAlreadyLocked):
                    errorcode = BrokerConstants.AccountAlreadyLocked;
                    break;

                case nameof(BrokerConstants.PasswordAlreadySet):
                    errorcode = BrokerConstants.PasswordAlreadySet;
                    break;

                case nameof(BrokerConstants.DuplicateNameCode):
                    errorcode = BrokerConstants.DuplicateNameCode;
                    break;

                case nameof(BrokerConstants.CannotDeleteCategoryCode):
                    errorcode = BrokerConstants.CannotDeleteCategoryCode;
                    break;

                case nameof(MemberConstants.MemberSecurityQAForValidationEmptyErrorCode):
                    errorcode = MemberConstants.MemberSecurityQAForValidationEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberResponseForSecurityQAValidationEmptyErrorCode):
                    errorcode = MemberConstants.MemberResponseForSecurityQAValidationEmptyErrorCode;
                    break;

                case nameof(MemberConstants.AccumulatorServiceGetIndividualAccumulatorDetailsInputEmptyErrorCode):
                    errorcode = MemberConstants.AccumulatorServiceGetIndividualAccumulatorDetailsInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberClaimsRequestObjectEmptyErrorCode):
                    errorcode = MemberConstants.MemberClaimsRequestObjectEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberUserIdForDashboardEmptyErrorCode):
                    errorcode = MemberConstants.MemberUserIdForDashboardEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberNoMemberAndDependentsErrorCode):
                    errorcode = MemberConstants.MemberNoMemberAndDependentsErrorCode;
                    break;

                case nameof(MemberConstants.MemberUserIdForMemberDetailsEmptyErrorCode):
                    errorcode = MemberConstants.MemberUserIdForMemberDetailsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberClaimsDataNotFoundErrorCode):
                    errorcode = MemberConstants.MemberClaimsDataNotFoundErrorCode;
                    break;

                case nameof(MemberConstants.MemberNoMemberDetailsErrorCode):
                    errorcode = MemberConstants.MemberNoMemberDetailsErrorCode;
                    break;

                case nameof(MemberConstants.DashboardServiceGetMemberPlanTypesInputEmptyErrorCode):
                    errorcode = MemberConstants.DashboardServiceGetMemberPlanTypesInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.DashboardServiceGetSecurityQuestionsByUserIdInputEmptyErrorCode):
                    errorcode = MemberConstants.DashboardServiceGetSecurityQuestionsByUserIdInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.DashboardServiceUpdateSecurityQuestionsByUserIdInputEmptyErrorCode):
                    errorcode = MemberConstants.DashboardServiceUpdateSecurityQuestionsByUserIdInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberUserIdForMemberDemographicsEmptyErrorCode):
                    errorcode = MemberConstants.MemberUserIdForMemberDemographicsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDemographicsForBrokerDemographicsUpdateEmptyErrorCode):
                    errorcode = MemberConstants.MemberDemographicsForBrokerDemographicsUpdateEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberUpdateBrokerDemographicsEmptyErrorCode):
                    errorcode = MemberConstants.MemberUpdateBrokerDemographicsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberPrimaryMemberForAddDependentEmptyErrorCode):
                    errorcode = MemberConstants.MemberPrimaryMemberForAddDependentEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDependentDetailIdForDeleteDependentEmptyErrorCode):
                    errorcode = MemberConstants.MemberDependentDetailIdForDeleteDependentEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDeleteDependentStatusEmptyErrorCode):
                    errorcode = MemberConstants.MemberDeleteDependentStatusEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberSecurityQuestionsEmptyErrorCode):
                    errorcode = MemberConstants.MemberSecurityQuestionsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberAddDependentStatusEmptyErrorCode):
                    errorcode = MemberConstants.MemberAddDependentStatusEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberStatesEmptyErrorCode):
                    errorcode = MemberConstants.MemberStatesEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MasterServiceUpdateDigitalDocumentIdInputEmptyErrorCode):
                    errorcode = MemberConstants.MasterServiceUpdateDigitalDocumentIdInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDocumentSearchFilterEmptyErrorCode):
                    errorcode = MemberConstants.MemberDocumentSearchFilterEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDocumentSearchResultEmptyErrorCode):
                    errorcode = MemberConstants.MemberDocumentSearchResultEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberDocumentSectionListEmptyErrorCode):
                    errorcode = MemberConstants.MemberDocumentSectionListEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberFAQCategoryListEmptyErrorCode):
                    errorcode = MemberConstants.MemberFAQCategoryListEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberFAQCategorySearchIdEmptyErrorCode):
                    errorcode = MemberConstants.MemberFAQCategorySearchIdEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberFAQListEmptyErrorCode):
                    errorcode = MemberConstants.MemberFAQListEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberFeedbackForSaveEmptyErrorCode):
                    errorcode = MemberConstants.MemberFeedbackForSaveEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceGetMemberMessagesInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceGetMemberMessagesInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceGetUnreadMessageCountInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceGetUnreadMessageCountInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceMarkMessageAsReadInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceMarkMessageAsReadInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceDeleteMessageInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceDeleteMessageInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceMarkMessageAsArchivedInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceMarkMessageAsArchivedInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MessageServiceGetMemberMessagesByPageInputEmptyErrorCode):
                    errorcode = MemberConstants.MessageServiceGetMemberMessagesByPageInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberMemberForRegistrationEmptyErrorCode):
                    errorcode = MemberConstants.MemberMemberForRegistrationEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberStatusForMemberRegisterMemberEmptyErrorCode):
                    errorcode = MemberConstants.MemberStatusForMemberRegisterMemberEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberRegistrationServiceSendOtpToMemberInputEmptyErrorCode):
                    errorcode = MemberConstants.MemberRegistrationServiceSendOtpToMemberInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.PaymentServiceUpdatePaymentInformationInputEmptyErrorCode):
                    errorcode = MemberConstants.PaymentServiceUpdatePaymentInformationInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.PaymentServiceGetPaymentInformationInputEmptyErrorCode):
                    errorcode = MemberConstants.PaymentServiceGetPaymentInformationInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.PaymentServiceGetPaymentReceiptDetailsInputEmptyErrorCode):
                    errorcode = MemberConstants.PaymentServiceGetPaymentReceiptDetailsInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.PlanDetailsServiceGetMemberProductGuideBookInputEmptyErrorCode):
                    errorcode = MemberConstants.PlanDetailsServiceGetMemberProductGuideBookInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.ProviderSearchServiceGetProviderSearchUrlInputEmptyErrorCode):
                    errorcode = MemberConstants.ProviderSearchServiceGetProviderSearchUrlInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberUsernameForSecurityQuestionsEmptyErrorCode):
                    errorcode = MemberConstants.MemberUsernameForSecurityQuestionsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberResponseForGetSecurityQuestionsEmptyErrorCode):
                    errorcode = MemberConstants.MemberResponseForGetSecurityQuestionsEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberParametersForSendPasswordResetConfirmationEmailEmptyErrorCode):
                    errorcode = MemberConstants.MemberParametersForSendPasswordResetConfirmationEmailEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberResponseForSendPasswordResetConfirmationEmailEmptyErrorCode):
                    errorcode = MemberConstants.MemberResponseForSendPasswordResetConfirmationEmailEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberUserIdForSendMemberAccountLockEmailEmptyErrorCode):
                    errorcode = MemberConstants.MemberUserIdForSendMemberAccountLockEmailEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberResponseForSendMemberAccountLockEmailEmptyErrorCode):
                    errorcode = MemberConstants.MemberResponseForSendMemberAccountLockEmailEmptyErrorCode;
                    break;

                case nameof(MemberConstants.MemberVerifyServiceIsMemberValidInputEmptyErrorCode):
                    errorcode = MemberConstants.MemberVerifyServiceIsMemberValidInputEmptyErrorCode;
                    break;

                case nameof(MemberConstants.DashboardServiceIsGroupMemberInputEmptyErrorCode):
                    errorcode = MemberConstants.DashboardServiceIsGroupMemberInputEmptyErrorCode;
                    break;
                //admin exceptions
                case nameof(AdminConstants.INVALID_ROLE_ID):
                    errorcode = AdminConstants.INVALID_ROLE_ID;
                    break;
                case nameof(AdminConstants.DUPLICATE_ROLENAME):
                    errorcode = AdminConstants.DUPLICATE_ROLENAME;
                    break;
                case nameof(AdminConstants.INACTIVATION_ERROR):
                    errorcode = AdminConstants.INACTIVATION_ERROR;
                    break;  
                default:
                    errorcode = 500;
                    break;
            }
            return errorcode;
        }
    }
}