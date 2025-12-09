using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entities;
using Vocabulary.Application.Interfaces;

namespace Vocabulary.Infrastructure.Data;

public class VocabularyDbContext : DbContext, IVocabularyDbContext
{
    public VocabularyDbContext(DbContextOptions<VocabularyDbContext> options)
        : base(options)
    {
    }

    public DbSet<UserWordList> UserWordLists => Set<UserWordList>();
    public DbSet<UserWord> UserWords => Set<UserWord>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserWordList>()
            .HasMany(x => x.UserWords)
            .WithOne()
            .HasForeignKey(x => x.UserWordListId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
