using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entities;

namespace Vocabulary.Infrastructure.Data;

public class VocabularyDbContext : DbContext
{
    public VocabularyDbContext(DbContextOptions<VocabularyDbContext> options)
        : base(options)
    {
    }

    public DbSet<WordList> WordLists => Set<WordList>();
    public DbSet<Word> Words => Set<Word>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WordList>()
            .HasMany(x => x.Words)
            .WithOne()
            .HasForeignKey(x => x.WordListId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
