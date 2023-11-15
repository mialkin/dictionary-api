using Dictionary.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.Infrastructure.Interfaces.Database;

public interface IDatabaseContext
{
    DbSet<Word> Words { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
