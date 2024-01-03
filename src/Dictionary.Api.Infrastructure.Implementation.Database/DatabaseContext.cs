using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Implementation.Database.EntityTypeConfigurations;
using Dictionary.Api.Infrastructure.Interfaces.Database;
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
        new(languageId, name, translation, transcription);
}
