namespace Jwt;

/// <summary>
/// 
/// </summary>
public interface IJwtSetting
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SignKey { get; set; }
}