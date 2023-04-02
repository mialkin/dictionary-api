using System.Threading;
using System.Threading.Tasks;

namespace Dictionary.Api.Infrastructure.Interfaces.Database;

public interface IDatabaseContext : IReadOnlyDatabaseContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
