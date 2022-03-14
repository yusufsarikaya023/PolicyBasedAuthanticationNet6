namespace Jwt;
/// <summary>
/// Response Token for Jwt Token Generation
/// </summary>
public class ResponseToken
{
    public string Token { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Id { get; set; }
    public DateTime ExpireDate { get; set; }
}