using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TextGen.Application.Interfaces;
using TextGen.Application.Services;
using TextGen.Infrastructure.Data;
using TextGen.Infrastructure.Services;

namespace TextGen.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        // 1. Veritabanı Servisleri 
        services.AddDbContext<TextGenDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<ITextGenDbContext>(provider =>
            provider.GetRequiredService<TextGenDbContext>());

        // 2. Dış Bağlantı Servisleri (LLM Client)
        services.AddScoped<ILlmClient, LlmClient>();

        return services;
    }
}