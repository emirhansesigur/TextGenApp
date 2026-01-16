using Auth.Application.Interfaces;
using Auth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Data;

public class AuthDbContext : DbContext, IAuthDbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Temel ayarlar
        base.OnModelCreating(modelBuilder);

        // User Entity Konfigürasyonu (Fluent API)
        modelBuilder.Entity<User>(entity =>
        {
            // Primary Key
            entity.HasKey(e => e.Id);

            // Email Benzersiz Olmalı (Kritik!)
            entity.HasIndex(e => e.Email).IsUnique();

            // Kısıtlamalar
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);

            // PostgreSQL'de tablo adını küçük harf yapmak iyi bir pratiktir (opsiyonel)
            entity.ToTable("users");
        });
    }
}