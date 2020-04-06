using System;

namespace Aliera.Utilities.Constants
{
    public class BrokerConstants
    {
        public const string DEFAULT_PASSWORD_RESET_TOKEN_PROVIDER_NAME = "DefaultPasswordResetTokenProvider";

        #region Modules
        public const string MOD_MEMBER_STATUS_GRAPH = "MemberStatusGraph";
        public const string MOD_MEMBER_PRODUCT_GRAPH = "MemberProductGraph";
        public const string MOD_ENROLLMENT_CANCELLATION_GRAPH = "EnrollmentCancellationGraph";
        public const string MOD_DEMOGRAPHICS = "Demographics";
        public const string MOD_BROKER_SEARCH = "BrokerSearch";
        public const string MOD_MEMBER_SEARCH = "MemberSearch";

        public const string MOD_ENROLLMENT = "Enrollment";
        public const string MOD_ADMIN123 = "Admin123";
        public const string MOD_MEMBER_SIGNATURE = "MemberSignature";
        public const string MOD_LOGIN = "Login";

        #endregion Modules

        #region Broker Portal Error Codes
        public const string ERR_DEFAULT = "1000";
        public const string ERR_MEMBER_STATUS_GRAPH = "1002";
        public const string ERR_MEMBER_PRODUCT_GRAPH = "1003";
        public const string ERR_ENROLLMENT_CANCELLATION_GRAPH = "1004";
        public const string ERR_DEMOGRAPHICS = "1005";
        public const string ERR_BROKER_SEARCH = "1006";
        public const string ERR_MEMBER_SEARCH = "1007";
        public const string ERR_LOGIN = "1008";


        public const string ERR_ENROLLMENT = "2000";
        public const string ERR_ADMIN123 = "2001";
        public const string ERR_MEMBER_SIGNATURE = "2005";

        #endregion Broker Portal Error Codes

        #region Error Codes
        public const double ModelValidationErrorCode = 5000;
        public const double UserNotFoundErrorCode = 5001;
        public const double UserLockedOutErrorCode = 5002;
        public const double LastAttemptLeftErrorCode = 5003;
        public const double FirstAttemptFailedErrorCode = 5004.1;
        public const double SecondAttemptFailedErrorCode = 5004.2;
        public const double ThirdAttemptFailedErrorCode = 5004.3;
        public const double NoSecurityQuestionsErrorCode = 5005;
        public const double AnswersDoNotMatchErrorCode = 5006;
        public const double InvalidPasswordResetTokenErrorCode = 5007;
        public const double CurrentAndPreviousPasswordIsSameErrorCode = 5008;
        public const double InvalidCurrentPasswordErrorCode = 5009;
        public const double InvalidPortalId = 5010;
        public const double AccountLocked = 6009;
        public const double PasswordExpired = 6015;
        public const double BadRequest = 6008;
        public const double PasswordAlreadyExists = 6007;
        public const double PasswordShouldNotSameAsUserName = 6014;
        public const double InvalidCurrentPassword = 6012;
        public const double UserIdEmptyErrorCode = 5001.1;
        public const double PasswordEmptyErrorCode = 5001.2;
        public const double UserIdAndPasswordEmptyErrorCode = 5001.3;
        public const double AccountAlreadyLocked = 6004;
        public const double PasswordAlreadySet = 6017;
        public const double DuplicateNameCode = 6018;
        public const double CannotDeleteCategoryCode = 6019;
        #endregion Error Codes


        //public const string ModelValidationErrorMsg = "Input model is invalid. Please verify input data.";
        public const string UserNotFoundErrorMsg = "User not found in the system.";
        //public const string MemberSearchExceptionErrCode = "1000";
        //public const string BrokerSearchExceptionErrCode = "1001";
        //public const string BrokerDemographicsExceptionErrCode = "1005";
        //public const string BrokerDocumentFileExtentionExceptionErrCode = "1002";
        //public const string BrokerDocumentFileSizeExceptionErrCode = "1003";
        //public const string ModelValidationErrorMsgForDocumentSize = "File size is more than 100 Mb. Please verify file size.";
        public const string ModelValidationErrorMsgForDocumentExtention = "Invalid file extention.";
        //public const string DocumentAndFormListingErrorCode = "5100";
        //public const string DocumentAndFormSearchErrorCode = "5101";
        //public const string DocumentAndFormDownloadErrorCode = "5102";
        //public const string BrokerBulletInBoardErrCode = "1004";
        //public const string BrokerBulletInBoardErrorMsg = "Internal server error. Please contact Admin";
        //public const string BrokerDocumentExceptionErrCode = "1006";
        //public const string BrokerDocumentErrorMsg = "Internal server error. Please contact Admin";
        //public const string MemberQuestionnaireMsg = "Data not found for Provided Type.";
        //public const string MemberEnrollmentErrorMsg = "Internal server error. Please contact Admin";
        //public const string EMAIL_CONFIG_ERRCODE = "1006";
        //public const string EMAIL_CONFIG_ERROR_MSG = "Email cannot send.Please contact Amdin ";
        //public const string SMS_CONFIG_ERR_CODE = "1007";
        //public const string SMS_CONFIG_ERROR_MSG = "Sms cannot send.Please contact Amdin ";
        public const string STATUS_ACTIVE = "Active";
        public const string STATUS_INACTIVE = "Inactive";
        public const string BROKER_PRODUCTS_EXCEPTION = "1007";
        public const string TEXT_INDIVIDUAL = "per Month for Individual";
        public const string TEXT_FAMILY = "per Month for Family";
        public const string TEXT_INDIVIDUAL_ONE_DEP = "per Month for Individual plus 1 Dependent";
        public static string TEXT_INDIVIDUAL_ONE_CHILD = "per Month for Individual plus 1 Child";
        public const string MEMBER_VALIDATION_MSG = "No Data available";
        public const string TEXT_IndPlusChild = "Individual plus child";
        public const string TEXT_IndPlusSpouse = "Individual plus spouse";
        public const string TEXT_IndPlus1Dep = "Individual plus one dependent";



        public const string UNEXPECTED_ERROR = "Unexpected error";
        public const string CONTENT_TYPE = "application/json";
        public const string DEBUG = "Debug";
        public const string INFORMATION = "Information";
        public const string WARNING = "Warning";
        public const string SQL_SERVER = "SqlServer";
        public const string REQUEST_HEADERS = "RequestHeaders";
        public const string REQUEST_HOST = "RequestHost";
        public const string REQUEST_PROTOCOL = "RequestProtocol";
        public const string REQUEST_FORM = "RequestForm";

        public const Int32 PAGE_INDEX = 0;
        public const Int32 PAGE_SIZE = Int32.MaxValue;
        public const string ASCENDING = "asc";
        public const string DESCENDING = "desc";
        public const string FILE_NAME = "filename";
        public const string CATEGORY_LOWER = "category";
        public const string LAST_UPDATED_ON = "lastupdatedon";
        public const string FILE_CONTENT_TYPE = "application/octet-stream";
        public const string DATE_TIME_FORMAT = "MM/dd/yyyy hh:mm tt";
        public const string COUNTRY_CODE_USA = "USA";

        public static string PDF_EXTENTION = ".pdf";
        public static string PLAN_CATEGORY = "Prima";

        public const string ADMIN123_ERROR_MSG = "Member enrollment at Admin123 is not successful, Please try again";

        public static string PRODUCT = "Product";
        public static string APPLICATION_FEE = "Application Fee";
        public static string PERCENT = "percent";
        public static string DOLLAR = "dollar";

        public const string TokenScheme = "Bearer";
        public const string AccessToken = "access_token";
        public const string BrokerApi = "brokerapi";
        public const string MemberApi = "memberapi";
        public const string AdminApi = "adminapi";
        public const string Id = "EntityId";

        public const string Broker_ContactInfo_OfficeLocations = "Broker_ContactInfo_OfficeLocations";
        public const string Broker_ContactInfo_PhoneNumbers = "Broker_ContactInfo_PhoneNumbers";
        public const string Broker_ContactInfo_Email = "Broker_ContactInfo_Email";
        public const string Broker_ContactInfo_MemberServices = "Broker_ContactInfo_MemberServices";
        public const string Broker_IsWebsiteActive = "Active";
        public const string Broker_IsWebsiteInActive = "InActive";

        public const string SECTION_DISPLAY_NAME = "sectiondisplayname";

        public const string SUNDAY = "sunday";
        public const string MONDAY = "monday";
        public const string TUESDAY = "tuesday";
        public const string WEDNESDAY = "wednesday";
        public const string THURSDAY = "thursday";
        public const string FRIDAY = "friday";
        public const string SATURDAY = "saturday";

        public const string DMS_IsDocumentAdmin = "IsDocumentAdmin";

        public const string TXT_CREATEPROTECTORPURPOSE = "Broker Protection";

    }
}
