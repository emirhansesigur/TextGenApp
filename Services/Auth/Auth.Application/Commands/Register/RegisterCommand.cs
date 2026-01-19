using Auth.Application.Interfaces;
using Auth.Application.Models;
using Auth.Application.Services;
using Auth.Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Commands.Register;

// 1. Command Modeli
public class RegisterCommand : IRequest<AuthResponseModel>
{
    public string FullName { get; set; } = null!; // Entity'de zorunlu olduğu için ekledik
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

// 2. Handler
// Primary Constructor kullanarak DI işlemini sınıf tanımında yaptık.
public class RegisterCommandHandler(
    IAuthDbContext _context,
    IAuthService _authService
    ) : IRequestHandler<RegisterCommand, AuthResponseModel>
{
    public async Task<AuthResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // A) Email Kontrolü (Aynı mail ile kayıt engellenmeli)
        var userExists = await _context.Users
            .AnyAsync(x => x.Email == request.Email, cancellationToken);

        if (userExists)
        {
            // Gerçek projede burada özel bir Exception fırlatılır (örn: BadRequestException)
            throw new Exception("Bu email adresi zaten kullanımda.");
        }

        // B) Şifre Hashleme (Logic Service'e devredildi)
        _authService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

        // C) Manuel Mapping (Command -> Entity)
        var user = new User
        {
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            RefreshToken = _authService.GenerateRefreshToken(),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7), // Refresh token 7 gün geçerli
            CreatedAt = DateTime.UtcNow
        };

        // D) Veritabanına Kayıt
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        // E) Token Üretimi
        var accessToken = _authService.CreateToken(user);

        // F) Response Dönüşü
        return new AuthResponseModel
        {
            Email = user.Email,
            Role = "User", // Şimdilik default rol
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken
        };
    }
}