using Auth.Core.Entities;

namespace Auth.Application.Services;

public interface IAuthService
{
    // Şifreyi güvenli hale getirmek için
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);

    // Kullanıcı doğrulandığında token üretmek için
    string CreateToken(User user);

    // Refresh token üretmek için
    string GenerateRefreshToken();
    bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt);
}
