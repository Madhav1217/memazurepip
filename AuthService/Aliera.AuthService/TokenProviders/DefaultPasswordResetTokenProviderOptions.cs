using Microsoft.AspNetCore.Identity;
using System;
using Constants = Aliera.Utilities.Constants;

namespace Aliera.AuthService.TokenProviders
{
    public class DefaultPasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public DefaultPasswordResetTokenProviderOptions()
        {
            Name = Constants.IdentityConstants.DefaultPasswordResetTokenProviderName;
            TokenLifespan = TimeSpan.FromDays(1);
        }
    }
}