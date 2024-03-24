using Dictionary.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dictionary.Api.Infrastructure.Implementation.Database.EntityTypeConfigurations;

public class WordEntityTypeConfiguration : IEntityTypeConfiguration<Word>
{
    public void Configure(EntityTypeBuilder<Word> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => new { x.LanguageId, x.Name }).IsUnique(); // TODO Add UserId uniqueness
        builder.Property(x => x.GenderMasculine).HasDefaultValue(false);
        builder.Property(x => x.GenderFeminine).HasDefaultValue(false);
        builder.Property(x => x.GenderNeuter).HasDefaultValue(false);
    }
}
