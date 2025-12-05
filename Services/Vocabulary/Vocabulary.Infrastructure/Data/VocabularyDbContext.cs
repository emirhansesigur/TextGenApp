using Microsoft.EntityFrameworkCore;
using Vocabulary.Core.Entities;

namespace Vocabulary.Infrastructure.Data;

public class VocabularyDbContext : DbContext
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
