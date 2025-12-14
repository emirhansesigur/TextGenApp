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
    public DbSet<GeneratedTextRequest> GeneratedTextRequests => Set<GeneratedTextRequest>();
    public DbSet<GeneratedTextKeyword> GeneratedTextKeywords => Set<GeneratedTextKeyword>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneratedText>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Content).IsRequired();

            // GeneratedText -> GeneratedTextRequest (1-e-1 İlişki)
            entity.HasOne(x => x.Request)
                  .WithOne(x => x.GeneratedText)
                  .HasForeignKey<GeneratedTextRequest>(x => x.GeneratedTextId);
        });

        modelBuilder.Entity<GeneratedTextRequest>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Level).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Topic).HasMaxLength(100).IsRequired();

            // YENİ İLİŞKİ: Request -> Keywords (1-e-Çok)
            entity.HasMany(x => x.Keywords)
                  .WithOne(x => x.GeneratedTextRequest)
                  .HasForeignKey(x => x.GeneratedTextRequestId) // FK artık RequestId
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GeneratedTextKeyword>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Keyword).HasMaxLength(100).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
