using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Vocabulary.Application.Interfaces;
using Vocabulary.Infrastructure.Data;
using System.Text;

namespace Vocabulary.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString, string jwtSecretKey)
    {
        services.AddDbContext<VocabularyDbContext>(options =>
                options.UseNpgsql(connectionString));

        // Context'i Arayüze yönlendir
        services.AddScoped<IVocabularyDbContext>(provider =>
            provider.GetRequiredService<VocabularyDbContext>());

        // 2. JWT Authentication (Program.cs'ten buraya taşıdık)
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        return services;
    }
}
