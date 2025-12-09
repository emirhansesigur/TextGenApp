using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entities;

namespace Vocabulary.Application.Interfaces;

public interface IVocabularyDbContext
{
    DbSet<UserWordList> UserWordLists { get; }
    DbSet<UserWord> UserWords { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}