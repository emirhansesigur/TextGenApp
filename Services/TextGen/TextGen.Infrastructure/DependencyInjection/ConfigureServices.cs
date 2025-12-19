using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

    public static IServiceCollection AddInfrastructureHangfire(this IServiceCollection services, string connectionString)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UsePostgreSqlStorage(options =>
            {
                options.UseNpgsqlConnection(connectionString);
            }));

        // Hangfire işlerini yürütecek sunucuyu ekle
        services.AddHangfireServer();

        return services;
    }
}