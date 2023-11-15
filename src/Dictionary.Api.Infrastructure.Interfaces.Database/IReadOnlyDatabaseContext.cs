using Dictionary.Api.Domain.Entities;

namespace Dictionary.Api.Infrastructure.Interfaces.Database;

public interface IReadOnlyDatabaseContext
{
    IQueryable<Word> Words { get; }

    // TODO Implement master-slave replication
}
