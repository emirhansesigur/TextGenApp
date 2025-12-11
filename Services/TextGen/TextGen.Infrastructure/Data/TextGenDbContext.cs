using Microsoft.EntityFrameworkCore;
using TextGen.Application.Services;
using TextGen.Core.Entities;

namespace TextGen.Infrastructure.Data;

public class TextGenDbContext : DbContext, ITextGenDbContext
{
    public TextGenDbContext(DbContextOptions<TextGenDbContext> options)
        : base(options)
    {
    }

    public DbSet<GeneratedText> GeneratedTexts => Set<GeneratedText>();
    public DbSet<GeneratedTextKeyword> GeneratedTextKeywords => Set<GeneratedTextKeyword>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneratedText>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasMany(x => x.Keywords)
                  .WithOne(x => x.GeneratedText)
                  .HasForeignKey(x => x.GeneratedTextId);
        });

        modelBuilder.Entity<GeneratedTextKeyword>(entity =>
        {
            entity.HasKey(x => x.Id);
        });
    }
}
