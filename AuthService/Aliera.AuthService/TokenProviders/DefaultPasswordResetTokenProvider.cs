using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Aliera.AuthService.TokenProviders
{
    public class DefaultPasswordResetTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public DefaultPasswordResetTokenProvider(IDataProtectionProvider dataProtectionProvider,
            IOptions<DefaultPasswordResetTokenProviderOptions> options)
            : base(dataProtectionProvider, options)
        {
        }
    }
}
