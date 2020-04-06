namespace Aliera.Utilities.Constants
{
    public class AuthConstants
    {
        public static double MoreThan10DaysLeftForPasswordExpire = 5011;
        public static double Last10DaysLeftForPasswordExpire = 5011.10;
        public static double Last9DaysLeftForPasswordExpire = 5011.9;
        public static double Last8DaysLeftForPasswordExpire = 5011.8;
        public static double Last7DaysLeftForPasswordExpire = 5011.7;
        public static double Last6DaysLeftForPasswordExpire = 5011.6;
        public static double Last5DaysLeftForPasswordExpire = 5011.5;
        public static double Last4DaysLeftForPasswordExpire = 5011.4;
        public static double Last3DaysLeftForPasswordExpire = 5011.3;
        public static double Last2DaysLeftForPasswordExpire = 5011.2;
        public static double Last1DaysLeftForPasswordExpire = 5011.1;
        public static double UserLockedOutErrorCode = 5002;
        public static double LastAttemptLeftErrorCode = 5003;
        public static double FirstAttemptFailedErrorCode = 5004.1;
        public static double SecondAttemptFailedErrorCode = 5004.2;
        public static double ThirdAttemptFailedErrorCode = 5004.3;
        public const double UserNotFoundErrorCode = 5001;
        public const double UserIdEmptyErrorCode = 5001.1;
        public const double PasswordEmptyErrorCode = 5001.2;
        public const double UserIdAndPasswordEmptyErrorCode = 5001.3;
        public const double UserObjectIsEmpty = 5001.4;
        public const double RegistrationFailed = 5001.5;
        public const double DeleteUserFailed = 5001.6;
        public const double ResetPasswordFailed = 5001.7;
        public const double PasswordShouldNotSameAsUserName = 6014;
        public const double PasswordAlreadyExists = 6007;
        public const double ChangePasswordFailed = 5001.10;
        public const double InvalidCurrentPassword = 6012;

        public static string ApplicationObjectNull = "Application Object is Null";
        public static string QuestionAnswerObjectNull = "Questions and Answers Object is Null";
        public static string AnswersNotMatched = "Answers are not matchingl";
        public static string ResetPassword = "ResetPassword";
        public static string IsPasswordExpired = "is_password_expired";
        public static string IsFirstLogin = "is_first_login";
        public static string Id = "id";
        public static string Local = "local";
    }

    public class IdentityConstants
    {
        public static string ClientId = "resourceownerclient";
        public static string ClientSecret = "secret";
        public static string MemberApiAllowedScopes = "memberapi";
        public static string BrokerApiAllowedScopes = "brokerapi";
        public static string MemberApiDisplayName = "Member API";
        public static string Code = "Code";
        public static string AccountLocked = "Account Locked";
        public static string PasswordExpired = "Password Expired";
        public static string Password = "password";
        public static string InvalidCredentials = "Invalid Login UserName or Password.Please try again";
        public static string ExpiresAt = "expires_at";
        public static string BrokerApiDisplayName = "Broker API";
        public static string DefaultPasswordResetTokenProviderName = "DefaultPasswordResetTokenProvider";
        public static string AccountAlreadyLocked = "Account Already Locked";
        public static string AdminApiAllowedScopes = "adminapi";
        public static string AdminApiDisplayName = "Admin API";
        public static string AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+ `~!#$%^&*()+=[]{}';:,|<>?\\/\"";
    }
}