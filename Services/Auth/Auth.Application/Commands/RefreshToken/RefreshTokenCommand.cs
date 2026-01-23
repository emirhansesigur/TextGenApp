using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Auth.Application.Commands.RefreshToken;

public class RefreshTokenCommand : IRequest<AuthResponseModel>
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenCommandHandler(
    IAuthDbContext _context,
    IAuthService _authService
    ) : IRequestHandler<RefreshTokenCommand, AuthResponseModel>
{
    public async Task<AuthResponseModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        // A) Süresi dolmuş token kime ait?
        var principal = _authService.GetPrincipalFromExpiredToken(request.AccessToken);
        if (principal == null)
        {
            throw new Exception("Geçersiz Access Token veya Refresh Token");
        }

        // B) User ID'yi çek
        var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        // C) Kullanıcıyı DB'den bul (RefreshToken ile birlikte)
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

        // D) Kontroller
        // 1. Kullanıcı var mı?
        // 2. DB'deki Refresh Token ile gelen aynı mı?
        // 3. Refresh Token süresi dolmuş mu?
        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new Exception("Geçersiz veya süresi dolmuş Refresh Token. Lütfen tekrar giriş yapın.");
        }

        // E) Her şey yolunda -> YENİ Tokenları Üret
        var newAccessToken = _authService.CreateToken(user);
        var newRefreshToken = _authService.GenerateRefreshToken();

        // F) DB'yi Güncelle (Eski refresh token'ı sil, yenisini yaz)
        user.RefreshToken = newRefreshToken;
        // User aktif olduğu sürece süreyi uzatıyoruz (Örn: +7 gün daha)
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponseModel
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Email = user.Email,
            Role = "User" // İstersen principal'dan claim olarak da okuyabilirsin
        };
    }
}