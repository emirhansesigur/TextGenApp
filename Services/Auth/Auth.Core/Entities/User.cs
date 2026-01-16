namespace Auth.Core.Entities;

public class User : BaseEntity
{
    public required string Email { get; set; }
    public required string FullName { get; set; }

    // Güvenlik: Şifreyi hashleyerek saklayacağız.
    public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
    public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

    // Mobil Uyumluluk (Refresh Token):
    // JWT kısa ömürlüdür (Access Token). Uzun ömürlü oturum için bu alanı kullanacağız.
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}