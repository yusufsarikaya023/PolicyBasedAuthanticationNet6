using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Model;

namespace Jwt;

/// <summary>
/// Compute Jwt Token for the user
/// </summary>
public static class JwtComputeService
{
    public static IEnumerable<Claim> Claims(User user) => new[] {
        new Claim("Id",user.Id.ToString()),
        new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Name),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim(ClaimTypes.Role, user.Roles.Select(r => r).Aggregate((a, b) => $"{a},{b}")),
        new Claim(ClaimTypes.Expiration, DateTime.Now.AddDays(1).ToString())
    };

    public static ResponseToken Compute(IJwtSetting setting, User user)
    {
        var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(setting.SignKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
          issuer: setting.Issuer,
          audience: setting.Audience,
          claims: Claims(user),
          expires: DateTime.Now.AddDays(1),
          signingCredentials: creds
        );

        string TokenContext = new JwtSecurityTokenHandler().WriteToken(token);
        ResponseToken response = new();
        response.Token = TokenContext;
        response.Name = user.Name;
        response.Email = user.Email;
        response.Id = user.Id.ToString();

        return response;
    }
}