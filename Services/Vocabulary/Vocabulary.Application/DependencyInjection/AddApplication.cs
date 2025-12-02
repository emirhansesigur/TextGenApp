using Microsoft.Extensions.DependencyInjection;
using Vocabulary.Application.Services;

namespace Vocabulary.Application.DependencyInjection;

public static class AddApplication
{
    public static IServiceCollection AddVocabularyApplication(this IServiceCollection services)
    {
        services.AddScoped<WordListService>();

        return services;
    }
}
