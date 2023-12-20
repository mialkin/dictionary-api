using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

internal class ReadOnlyDatabaseContext(IDatabaseContext databaseContext) : IReadOnlyDatabaseContext
{
    public IQueryable<Word> Words => databaseContext.Words.AsNoTracking();
}
