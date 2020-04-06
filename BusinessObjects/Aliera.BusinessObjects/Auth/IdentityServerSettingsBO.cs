namespace Aliera.BusinessObjects.Auth
{
    public class IdentityServerSettingsBO
    {
        public string UiUrl { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        public string AllowedCorsOrigins { get; set; }
        public string ClientUri { get; set; }
        public string PostLogoutRedirectUrisUi { get; set; }
        public string RedirectUrisUi { get; set; }
        public string AuthUrl { get; set; }
        public string MemberRedirectUris { get; set; }
        public string MemberPostLogoutRedirectUris { get; set; }
        public string MemberAllowedCorsOrigins { get; set; }
        public string MemberClientUri { get; set; }
        public string IssuerUri { get; set; }
        public string Authority { get; set; }
        public int AccessTokenLifeTime { get; set; }
        public int ResetPasswordChangeDurationInDays { get; set; }
        public int ResetPasswordNotificationEnableInDays { get; set; }
        public int PasswordRequiredLength { get; set; }
        public int MaxFailedAccessAttempts { get; set; }
    }
}