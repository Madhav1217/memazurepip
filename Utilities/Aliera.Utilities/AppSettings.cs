namespace Aliera.Utilities
{
    public class AppSettings
    {
        public string AllowedDocumentType { get; set; }
        public string BrokerDocumentPath { get; set; }
        public string DocumentAndFormPath { get; set; }
        public int TobaccoUserFee { get; set; }
        public int FamilyRiderFee { get; set; }
        public string ProductPath { get; set; }
        public string PlanPath { get; set; }
        public int PortNumber { get; set; }
        public string SmtpServer { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressVerificationUrl { get; set; }
        public string LogoPath { get; set; }
        public string SingnatureDocumentPath { get; set; }
        public string AllowedSignatureDocumentType { get; set; }
        public string LinkValidity { get; set; }
        public string InvoicePath { get; set; }
        public string OtpMailSubject { get; set; }
        public string LoginPath { get; set; }
        public string ServerIP { get; set; }
        public string PasswordResetPage { get; set; }
        public string CountryCode { get; set; }
        public string MemberSummaryPageUrl { get; set; }

        //public string LibwkhtmltoxPath { get; set; }
        public string Admin123Url { get; set; }

        public string Admin123User { get; set; }
        public string Admin123Password { get; set; }
        public int PeriodId { get; set; }
        public long CorpId { get; set; }
        public string Lead { get; set; }
        public long AgentId { get; set; }
        public string SingnatureDocumentFTPPath { get; set; }
        public string FTPUsername { get; set; }
        public string FTPPassword { get; set; }
        public string ApplicationName { get; set; }
        public string AuditUri { get; set; }
        public string AuditRequestApi { get; set; }
        public int AuditQueueLimit { get; set; }
        public int AuditSetTimeout { get; set; }
        public string ResetPasswordUrl { get; set; }
        public string LoginUrl { get; set; }
        public string AuthUrl { get; set; }
        public string MemberGuideBookPath { get; set; }
        public string EnrollmentPageUrl { get; set; }
        public string AlieraMarketingEmail { get; set; }
        public string MemberServicesEmail { get; set; }
        public string PlanChangeRequestEmail { get; set; }
        public int SessionIdleTime { get; set; }
        public static string Environment { get; set; }
        public string S3UploadDocumentUri { get; set; }
        public string S3PublicDocumentUri { get; set; }
        public string S3RenameDocumentUri { get; set; }
        public string Admin123BrokerDemographicsUrl { get; set; }
        public string Admin12BrokerDemographicsUser { get; set; }
        public string Admin123BrokerDemographicsPassword { get; set; }
        public string RHAPIKey { get; set; }
        public string RHGatewayURL { get; set; }
        public string RedirectURLForRH { get; set; }
        public string UserCreationAPI { get; set; }
        public string RHUserName { get; set; }
        public string RHPassword { get; set; }
        public string RHKeyID { get; set; }
        public string RHQueryAPIURL { get; set; }

        public string MemberResetPasswordUrl { get; set; }
        public string MemberPhysicalIDCradRequestEmail { get; set; }
        public int SessionTimeOut { get; set; }
        public string UpdatePasswordAPI { get; set; }
        public string VerifyBrokerURL { get; set; }
        public string MemberLoginURL { get; set; }
    }
}