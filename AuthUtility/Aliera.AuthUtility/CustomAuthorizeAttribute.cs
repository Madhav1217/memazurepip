using System;
using Aliera.DatabaseEntities.Models.Auth;
using Aliera.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;

namespace Aliera.AuthUtility
{
    public class CustomAuthorizeAttribute : TypeFilterAttribute
    {
        public CustomAuthorizeAttribute() : base(typeof(CustomAuthorizeFilter))
        {

        }
    }

    public class CustomAuthorizeFilter : IAuthorizationFilter
    {
        private readonly IUnitOfWork<AuthContext> _authUnitOfWork;
        public CustomAuthorizeFilter(IUnitOfWork<AuthContext> unitOfWork)
        {
            _authUnitOfWork = unitOfWork;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (((HttpRequestHeaders)((DefaultHttpRequest)((DefaultHttpContext)context.HttpContext).Request).Headers).HeaderAuthorization.Count > 0)
            {
                var accessToken = ((HttpRequestHeaders)((DefaultHttpRequest)((DefaultHttpContext)context.HttpContext).Request).Headers).HeaderAuthorization[0];
                if (accessToken != null && !String.IsNullOrEmpty(accessToken))
                {
                    accessToken = accessToken.Split(' ')[1];
                    if (accessToken != "null")
                    {
                        var userId = UtilityHelper.GetUserId(accessToken);
                        var tokens = _authUnitOfWork.GetRepository<Tokens>().GetFirstOrDefault(a => a, x => x.UserId == userId);

                        if (tokens != null)
                        {
                            if (tokens.AccessToken != accessToken || tokens.Validity < DateTime.UtcNow)
                            {
                                context.Result = new UnauthorizedResult();
                            }
                        }
                    }
                }
            }
        }
    }
}
