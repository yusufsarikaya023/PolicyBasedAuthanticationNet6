
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jwt;
public static class AddJwtTokenServicesExtension
{
    /// <summary>
    /// Add JwtTokenServices to the services collection and configure JwtBearerOptions 
    /// with the settings from the appsettings.json file.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddJwtTokenService(this IServiceCollection services, IConfiguration configuration)
    {
        JwtSetting setting = new();
        configuration.Bind("Jwt", setting);

        services.AddSingleton<IJwtSetting>(x => setting);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(setting.SignKey)),
                ValidateIssuer = true,
                ValidIssuer = setting.Issuer,
                ValidateAudience = true,
                ValidAudience = setting.Audience,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromDays(1),
            };
        });
    }
}
