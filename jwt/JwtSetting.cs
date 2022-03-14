namespace Jwt;

/// <summary>
/// Setting for Jwt Token Generation
/// </summary>
public class JwtSetting : IJwtSetting
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SignKey { get; set; }
}