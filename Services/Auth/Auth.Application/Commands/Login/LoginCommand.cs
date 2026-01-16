using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Commands.Login;

public class LoginCommand : IRequest<AuthResponseModel>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class LoginCommandHandler(IAuthDbContext _context, IAuthService _authService) : IRequestHandler<LoginCommand, AuthResponseModel>
{
    public async Task<AuthResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        // A) Kullanıcıyı Bul
        var user = await _context.Users
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        // Güvenlik Notu: Kullanıcı yoksa "Email hatalı" demek yerine genel mesaj verilir
        // ki kötü niyetli kişiler email taraması yapamasın.
        if (user == null)
        {
            throw new Exception("Kullanıcı adı veya şifre hatalı.");
        }

        // B) Şifre Doğrulama (Service katmanında yazdığımız metodu kullanıyoruz)
        if (!_authService.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new Exception("Kullanıcı adı veya şifre hatalı.");
        }

        // C) Token Üretimi
        var accessToken = _authService.CreateToken(user);
        var refreshToken = _authService.GenerateRefreshToken();

        // D) Refresh Token'ı Güncelle (Kritik Adım!)
        // Kullanıcı her login olduğunda yeni bir refresh token alır.
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        // E) Değişiklikleri Kaydet
        await _context.SaveChangesAsync(cancellationToken);

        // F) Cevabı Dön
        return new AuthResponseModel
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Email = user.Email,
            Role = "User" // Şimdilik sabit, ileride user.Role'den gelebilir
        };
    }
}