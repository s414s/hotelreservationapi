using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Domain.Enum;
using Microsoft.IdentityModel.Tokens;

namespace Presentation.Middlewares;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly string[] _allowedRoles;
    private readonly Roles _allowedRole;
    public AuthorizationFilter(string[] allowedRoles)
    {
        _allowedRoles = allowedRoles;
    }
    public AuthorizationFilter(Roles allowedRole)
    {
        _allowedRole = allowedRole;
        _allowedRoles = [];
    }
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claim = context.HttpContext.User.Claims
            .Where(claim => claim.Type.ToString() == "roles")
            .FirstOrDefault();

        if (claim == null)
        {
            context.Result = new UnauthorizedObjectResult(string.Empty);
            return;
        }
        else
        {
            var ClaimRoles = claim.Value.Split(',');
            if (ClaimRoles.Intersect(_allowedRoles!).ToArray().IsNullOrEmpty())
            {
                context.Result = new UnauthorizedObjectResult(string.Empty);
                return;
            }
        }
    }
}
