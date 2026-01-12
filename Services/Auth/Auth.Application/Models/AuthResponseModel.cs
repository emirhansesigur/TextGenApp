namespace Auth.Application.Models;

public class AuthResponseModel
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Email { get; set; } = null!;
}