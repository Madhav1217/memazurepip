using System.Collections.Generic;

namespace Aliera.BusinessObjects.Auth
{
    public class AccessTokenBO
    {
        public long UserId { get; set; }
        public int CustomHttpStatusCode { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public int ExpiresIn { get; set; }
        public string Error { get; set; }
        public string TokenType { get; set; }
        public string ErrorDescription { get; set; }
        public int LoginFailedCount { get; set; }
    }
}