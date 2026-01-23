using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Application.Services;
using Auth.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Infrastructure.Services;

public class AuthService(IConfiguration _configuration) : IAuthService
{
    // 1. Şifre Hashleme (Register için)
    public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key; // Algoritmanın ürettiği rastgele tuz (salt)
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    // 2. Şifre Doğrulama (Login için)
    public bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        using var hmac = new HMACSHA512(storedSalt); // Kayıtlı tuzu kullan
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Byte byte karşılaştırma (SequenceEqual en hızlı yöntemdir)
        return computedHash.SequenceEqual(storedHash);
    }

    // 3. JWT Token Üretme
    public string CreateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        // appsettings.json'dan gizli anahtarı alıyoruz
        // Not: Gerçek projede bu key en az 64 karakter olmalı!
        var key = Encoding.ASCII.GetBytes(_configuration["JwtSecretKey"]!);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                // İleride Role tablosu gelirse buraya eklenecek, şimdilik statik
                new Claim(ClaimTypes.Role, "User")
            }),
            Expires = DateTime.UtcNow.AddMinutes(15), // Access Token ömrü kısa olmalı
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    // 4. Refresh Token (Kriptografik Rastgele String)
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecretKey"]!)),
            ValidateLifetime = false // ÖNEMLİ: Süresi dolmuş olsa da hata verme, izin ver
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            // Algoritma kontrolü (Güvenlik için şart)
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
        catch
        {
            return null; // Token bozuksa null dön
        }
    }
}