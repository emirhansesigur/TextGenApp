using Microsoft.EntityFrameworkCore;
using TextGen.Core.Entities;

namespace TextGen.Application.Services;

public interface ITextGenDbContext
{
    DbSet<GeneratedText> GeneratedTexts { get; }
    DbSet<GeneratedTextKeyword> GeneratedTextKeywords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
