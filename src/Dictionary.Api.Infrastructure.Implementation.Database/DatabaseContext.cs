using Dictionary.Api.Domain;
using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Dictionary.Api.Infrastructure.Interfaces.Database.EntityTypeConfigurations;
using Dictionary.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

internal class DatabaseContext : DbContext, IDatabaseContext
{
    public DbSet<Word> Words { get; set; }

#pragma warning disable CS8618
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
#pragma warning restore CS8618
    {
    }

    // TODO Add Languages table seeding. Use InMemoryCache for retrieving languages
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var utcNow = DateTime.UtcNow; // TODO Replace with ISystemClock?
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.Created(utcNow);
                    break;

                case EntityState.Modified:
                    entry.Entity.Updated(utcNow);
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WordEntityTypeConfiguration());

        // SeedWords(modelBuilder); TODO Make it work https://learn.microsoft.com/en-us/ef/core/modeling/data-seeding
    }

    private static void SeedWords(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Word>().HasData(
            CreateWord(1, "apple", null, "яблоко"),
            CreateWord(1, "orange", null, "апельсин"),
            CreateWord(1, "banana", "bə'nɑːnə", "банан")
        );
    }

    private static Word CreateWord(int languageId, string name, string? transcription, string translation) =>
        new(languageId, name, transcription, translation);
}
