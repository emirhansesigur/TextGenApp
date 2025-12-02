using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vocabulary.Core.Repositories;
using Vocabulary.Infrastructure.Data;
using Vocabulary.Infrastructure.Repositories;

namespace Vocabulary.Infrastructure.DependencyInjection;

public static class AddInfrastructure
{
    public static IServiceCollection AddVocabularyInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<VocabularyDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IWordListRepository, WordListRepository>();

        return services;
    }
}
