using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vocabulary.Application.Interfaces;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Infrastructure.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<VocabularyDbContext>(options =>
                options.UseNpgsql(connectionString));

        // Context'i Arayüze yönlendir
        services.AddScoped<IVocabularyDbContext>(provider =>
            provider.GetRequiredService<VocabularyDbContext>());
        
        return services;
    }
}
