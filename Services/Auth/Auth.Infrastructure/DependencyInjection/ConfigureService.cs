using Auth.Application.Interfaces;
using Auth.Application.Services;
using Auth.Infrastructure.Data;
using Auth.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.DependencyInjection;

public static class DependencyInjection
{

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AuthDbContext>(options =>
                options.UseNpgsql(connectionString));

        // Context'i Arayüze yönlendir
        services.AddScoped<IAuthDbContext>(provider =>
            provider.GetRequiredService<AuthDbContext>());

        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}