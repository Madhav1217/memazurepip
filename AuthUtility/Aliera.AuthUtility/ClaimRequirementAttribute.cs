using Aliera.BusinessObjects.Auth;
using Aliera.Utilities.Logging.CustomExceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Enum = Aliera.Utilities.Enumerations;

namespace Aliera.AuthUtility
{
    public sealed class ClaimRequirementAttribute : TypeFilterAttribute
    {
        public ClaimRequirementAttribute(string claimType, string claimValue, string claimValueType) : base(typeof(ClaimRequirementFilter))
        {
            Arguments = new object[] { new Claim(claimType, claimValue, claimValueType) };
        }
    }

    public class ClaimRequirementFilter : IAuthorizationFilter
    {
        readonly Claim _claim;

        public ClaimRequirementFilter(Claim claim)
        {
            _claim = claim;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var rolesJson = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == _claim.Type).Value;
            var objRoles = JsonConvert.DeserializeObject<List<CustomRolesBO>>(rolesJson);
            if (objRoles.Count > 0 && objRoles[0].Privileges != null && objRoles[0].Privileges.Count > 0)
            {
                var hasClaim = objRoles.Any(r => r.Privileges.Any(p => p.Name == _claim.Value && p.Permission.Any(pe => Convert.ToInt32(pe.GetType().GetProperty(_claim.ValueType).GetValue(pe)) == (int)Enum.Permission.Yes)));
                if (!hasClaim)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                throw new CustomException("User not associated with any roles");
            }
        }
    }
}
