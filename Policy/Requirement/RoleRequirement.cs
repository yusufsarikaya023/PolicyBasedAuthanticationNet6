using Microsoft.AspNetCore.Authorization;

namespace Policy;

public class RoleRequirement : IAuthorizationRequirement
{
    public RoleRequirement(string role)
    {
        Role = role;
    }

    public string Role { get; set; }
}