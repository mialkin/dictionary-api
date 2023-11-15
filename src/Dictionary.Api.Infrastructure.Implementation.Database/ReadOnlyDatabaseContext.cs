using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

internal class ReadOnlyDatabaseContext : IReadOnlyDatabaseContext
{
    private readonly DatabaseContext _databaseContext;

    public ReadOnlyDatabaseContext(DatabaseContext databaseContext) => _databaseContext = databaseContext;

    public IQueryable<Word> Words => _databaseContext.Words.AsNoTracking();
}
