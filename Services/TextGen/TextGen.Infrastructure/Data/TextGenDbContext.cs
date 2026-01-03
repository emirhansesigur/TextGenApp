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
    public DbSet<SuggestedTopic> SuggestedTopics => Set<SuggestedTopic>();
    public DbSet<PublicText> PublicTexts => Set<PublicText>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneratedText>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Content).IsRequired();

            entity.OwnsMany(x => x.Quiz, builder =>
            {
                builder.ToJson();
            });

            // GeneratedText -> GeneratedTextRequest (1-e-1 İlişki)
            entity.HasOne(x => x.Request)
                  .WithOne(x => x.GeneratedText)
                  .HasForeignKey<GeneratedTextRequest>(x => x.GeneratedTextId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GeneratedTextRequest>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Level).HasMaxLength(10).IsRequired();
            entity.Property(x => x.Topic).HasMaxLength(100).IsRequired();

            // YENİ İLİŞKİ: Request -> Keywords (1-e-Çok)
            entity.HasMany(x => x.Keywords)
                  .WithOne(x => x.GeneratedTextRequest)
                  .HasForeignKey(x => x.GeneratedTextRequestId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<GeneratedTextKeyword>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Keyword).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<SuggestedTopic>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Content).IsRequired();
            entity.Property(x => x.Category).IsRequired();
        });

        modelBuilder.Entity<PublicText>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Title).IsRequired();
            entity.Property(x => x.Category).IsRequired();
            entity.Property(x => x.TextLevel).IsRequired();
            entity.Property(x => x.Content).IsRequired();
            entity.Property(x => x.Summary).IsRequired();
            entity.Property(x=> x.WordCount).IsRequired();
            entity.Property(x=> x.EstimatedReadingTimeMinutes).IsRequired();
            entity.OwnsMany(x => x.Quiz, builder =>
            {
                builder.ToJson();
            });
        });

        base.OnModelCreating(modelBuilder);
    }
}
