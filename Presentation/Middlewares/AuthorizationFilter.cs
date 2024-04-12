using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Domain.Enum;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Presentation.Middlewares;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizationFilter : Attribute, IAuthorizationFilter
{
    private readonly Roles _allowedRole;

    public AuthorizationFilter(Roles allowedRole)
    {
        _allowedRole = allowedRole;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var test = context.HttpContext.User.Claims.ToList();

        var userRole = context.HttpContext.User.Claims
            .Where(claim => claim.Type.ToString() == "userrole")
            .FirstOrDefault()?
            .Value;

        if (userRole is string role && Enum.Parse<Roles>(role, true) == _allowedRole)
        {
            return;
        }

        context.Result = new UnauthorizedObjectResult(string.Empty);
        return;
    }
}
