using Dictionary.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Infrastructure.Interfaces.Database;

public interface IReadOnlyDatabaseContext
{
    DbSet<Word> Words { get; }
}
