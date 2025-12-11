using Microsoft.Extensions.DependencyInjection;
using TextGen.Application.Services;

namespace TextGen.Application.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // MediatR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);
        });

        // PromptBuilder gibi Application katmanına ait diğer servisler
        services.AddTransient<PromptBuilder>();

        return services;
    }
}