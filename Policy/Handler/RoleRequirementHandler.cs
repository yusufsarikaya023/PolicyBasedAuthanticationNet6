using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Policy;

public class RoleRequirementHandler : AuthorizationHandler<RoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleRequirement requirement)
    {
        IEnumerable<IAuthorizationRequirement> requirements = context.Requirements;
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.Role))
        {
            context.Fail(new AuthorizationFailureReason(this, "Role does not exist on the user."));
        }
        var role = context.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
        string[] roles = role.Split(',');

        if (!roles.Any(x=>requirements.Any(r=>r.GetType() == typeof(RoleRequirement) && ((RoleRequirement)r).Role == x)))
        {
            context.Fail(new AuthorizationFailureReason(this, "User does not have the required role."));
        }
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}