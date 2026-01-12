using Microsoft.EntityFrameworkCore;
using Auth.Application.Interfaces;
using Auth.Infrastructure.Data;
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

        return services;
        throw new NotImplementedException();
    }
}