using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.Utilities.Constants
{
    public class MemberConstants
    {
        //Member Claims Data
        public const double MemberClaimsRequestObjectEmptyErrorCode = 7000;

        public const double MemberClaimsDataNotFoundErrorCode = 7000.1;
        public const double MemberClaimsDocumentIdForDownloadEmptyErrorCode = 7001;
        public const double MemberClaimsEobIdentifiedEmptyErrorCode = 7002;

        //Member Dashboard
        public const double MemberUserIdForDashboardEmptyErrorCode = 7003;

        public const double MemberNoMemberAndDependentsErrorCode = 7004;
        public const double MemberUserIdForMemberDetailsEmptyErrorCode = 7005;
        public const double MemberNoMemberDetailsErrorCode = 7006;

        //Member Member Demographics
        public const double MemberUserIdForMemberDemographicsEmptyErrorCode = 7007;

        public const double MemberMemberDemographicsEmptyErrorCode = 7008;
        public const double MemberDemographicsForBrokerDemographicsUpdateEmptyErrorCode = 7009;
        public const double MemberUpdateBrokerDemographicsEmptyErrorCode = 7010;
        public const double MemberPrimaryMemberForAddDependentEmptyErrorCode = 7011;
        public const double MemberAddDependentStatusEmptyErrorCode = 7012;
        public const double MemberDependentDetailIdForDeleteDependentEmptyErrorCode = 7013;
        public const double MemberDeleteDependentStatusEmptyErrorCode = 7014;

        //Member Master Data
        public const double MemberSecurityQuestionsEmptyErrorCode = 7015;

        public const double MemberStatesEmptyErrorCode = 7016;

        //Member Document and Form
        public const double MemberGeneralDocumentPagingParameterEmptyErrorCode = 7017;

        public const double MemberGeneralDocumentEmptyErrorCode = 7018;
        public const double MemberDocumentSearchFilterEmptyErrorCode = 7019;
        public const double MemberDocumentSearchResultEmptyErrorCode = 7020;
        public const double MemberDocumentSectionListEmptyErrorCode = 7021;
        public const double MemberDocumentIdForDownloadEmptyErrorCode = 7022;
        public const double MemberDocumentForDownloadEmptyErrorCode = 7023;

        //Member FAQ
        public const double MemberFAQCategoryListEmptyErrorCode = 7024;

        public const double MemberFAQCategorySearchIdEmptyErrorCode = 7025;
        public const double MemberFAQListEmptyErrorCode = 7026;

        //Member Feedback
        public const double MemberFeedbackForSaveEmptyErrorCode = 7027;

        //Member Registration
        public const double MemberMemberForRegistrationEmptyErrorCode = 7028;

        public const double MemberStatusForMemberRegisterMemberEmptyErrorCode = 7029;
        public const double MemberMemberForValidationEmptyErrorCode = 7030;
        public const double MemberResponseForMemberValidationEmptyErrorCode = 7031;
        public const double MemberUsernameForCheckMemberUsernameEmptyErrorCode = 7032;
        public const double MemberResponseForCheckMemberUsernameEmptyErrorCode = 7033;
        public const double MemberParametersForSendOtpToMemberEmptyErrorCode = 7034;
        public const double MemberResponseForSendOtpToMemberEmptyErrorCode = 7035;
        public const double MemberParametersForValidateOtpEmptyErrorCode = 7036;
        public const double MemberResponseForValidateOtpEmptyErrorCode = 7037;

        //Member Reser Password
        public const double MemberUsernameForSecurityQuestionsEmptyErrorCode = 7038;

        public const double MemberResponseForGetSecurityQuestionsEmptyErrorCode = 7039;
        public const double MemberSecurityQAForValidationEmptyErrorCode = 7040;
        public const double MemberResponseForSecurityQAValidationEmptyErrorCode = 7041;
        public const double MemberParametersForSendPasswordResetConfirmationEmailEmptyErrorCode = 7042;
        public const double MemberResponseForSendPasswordResetConfirmationEmailEmptyErrorCode = 7043;
        public const double MemberUserIdForSendMemberAccountLockEmailEmptyErrorCode = 7044;
        public const double MemberResponseForSendMemberAccountLockEmailEmptyErrorCode = 7045;

        public const double MemberVerifyServiceIsUsernameAvailableInputEmptyErrorCode = 7046;
        public const double MemberVerifyServiceValidateOtpInputEmptyErrorCode = 7047;
        public const double AccumulatorServiceGetMemberAccumulatorDetailsInputEmptyErrorCode = 7048;
        public const double AccumulatorServiceGetIndividualAccumulatorDetailsInputEmptyErrorCode = 7049;
        public const double DashboardServiceGetMemberPlanTypesInputEmptyErrorCode = 7050;
        public const double DashboardServiceGetSecurityQuestionsByUserIdInputEmptyErrorCode = 7051;
        public const double DashboardServiceUpdateSecurityQuestionsByUserIdInputEmptyErrorCode = 7052;
        public const double DemographicsServiceGetPlanDetailsBasedOnUserIdInputEmptyErrorCode = 7053;
        public const double MasterServiceUpdateDigitalDocumentIdInputEmptyErrorCode = 7054;
        public const double MessageServiceGetMemberMessagesInputEmptyErrorCode = 7055;
        public const double MessageServiceGetMemberMessagesByPageInputEmptyErrorCode = 7056;
        public const double MessageServiceMarkMessageAsReadInputEmptyErrorCode = 7057;
        public const double MessageServiceDeleteMessageInputEmptyErrorCode = 7058;
        public const double MessageServiceMarkMessageAsArchivedInputEmptyErrorCode = 7059;
        public const double MessageServiceGetUnreadMessageCountInputEmptyErrorCode = 7060;
        public const double MemberRegistrationServiceSendOtpToMemberInputEmptyErrorCode = 7061;
        public const double ProviderSearchServiceGetProviderSearchUrlInputEmptyErrorCode = 7062;
        public const double PlanDetailsServiceGetMemberProductGuideBookInputEmptyErrorCode = 7063;
        public const double PaymentServiceGetPaymentInformationInputEmptyErrorCode = 7064;
        public const double PaymentServiceUpdatePaymentInformationInputEmptyErrorCode = 7065;
        public const double PaymentServiceGetPaymentReceiptDetailsInputEmptyErrorCode = 7066;
        public const double MemberVerifyServiceIsMemberValidInputEmptyErrorCode = 7067;
        public const double DashboardServiceIsGroupMemberInputEmptyErrorCode = 7068;
        public const double MemberIdCardServiceSendPhysicalIdCardRequestEmailInputEmptyErrorCode = 7069;
        public const double MemberSearchServiceGetMembersBySearchInputEmptyErrorCode = 7070;
        public const double MemberSearchServiceGetUserDetailsInputEmptyErrorCode = 7071;

        public const string RHErrorResponseCode = "300";
        public const string RHErrorSuccessResponseCode = "100";
        public const string DuplicateVaultId = "duplicate";

        //Member Plan types
        public const string MedicalPlan = "medical";

        public const string DentalPlan = "dental";
        public const string VisionPlan = "vision";
        public const string DentalVisionPlan = "dentalvision";

        //Member claims FilterAttribute
        public const string YearToDateFilter = "yeartodate";

        public const string LastThreeMonthsFilter = "lastthreemonths";
        public const string LastSixMonthsFilter = "lastsixmonths";
        public const string LastTweleveMonthsFilter = "lasttwelevemonths";

        //Member digital document type constants
        public const string ProductGuideBook = "product";

        public const string ClaimsEOB = "claims";
        public const string AvatarImage = "avatarimage";
        public const string DigitalIDCard = "digitalid";

        //Member type constants
        public const string Self = "self";

        public const string Dependent = "dependent";
        public const string Primary = "primary";
        public const string Subscriber = "subscriber";

        //Member plan names constants
        public const string TrinityDentalVision = "trinity dental & vision";

        public const string NoRecordsFound = "No records found.";
        public const string UnexpectedCase = "Unexpected Case.";
        public const string PaidDate = "PaidDate";
        public const string PaymentMethod = "PaymentMethod";
        public const string Amount = "Amount";
        public const string GeneratedTime = "GeneratedTime";
        public const string EnrollmentNotAllowedStates = "EnrollmentNotAllowedStates";

        //ImageFormat constants
        public const string Png = "png";

        public const string Gif = "gif";
        public const string Tiff = "tiff";
        public const string Bmp = "bmp";
        public const string Jpeg = "jpeg";
        public const string Xwmf = "x-wmf";

        //Payment Receipt constants
        public const string Type = "SALE";

        public const string TransactionStatus = "Approved";

        //Roles Permissions
        public const int HasPermission = 1;

        public const int DontHavePermission = 0;
        public const string Roles = "roles";
        public const string MemberLeftPanPermission = "MEM_LEFT_PAN_MEMBER_AND_PROGRAM_DETAILS";
        public const string MemberSubscriberInfoPermission = "MEM_SUBSCRIBER_INFORMATION";
        public const string MemberDependentInfoPermission = "MEM_DEPENDENT_INFORMATION";
        public const string MemberServiceInfoPermission = "MEM_SERVICE_INFORMATION";
        public const string MemberIdCardsPermission = "MEM_DIGITAL_ID_CARDS";

        //Member Supplemental Id Card Constants
        public const string UnityGroup = "AHUNC";

        public const string UnitySingle = "AHUNI";
        public const string TrinityGroup = "AHTRC";
        public const string TrinitySingle = "AHTRI";

        public const string MemberInquiryType = "MemberInquiryType";

        public const int CallCenterInternal = 9;
        public const int CallCenterAliera = 10;
    }
}