using Microsoft.EntityFrameworkCore;
using TextGen.Core.Entities;

namespace TextGen.Application.Services;

public interface ITextGenDbContext
{
    DbSet<GeneratedText> GeneratedTexts { get; }
    DbSet<GeneratedTextKeyword> GeneratedTextKeywords { get; }
    DbSet<SuggestedTopic> SuggestedTopics {  get; }
    DbSet<PublicText> PublicTexts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
