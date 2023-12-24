using Ardalis.GuardClauses;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(
        this IServiceCollection services,
        PostgresSettings? postgresSettings)
    {
        Guard.Against.Null(postgresSettings);
        var connectionString = Guard.Against.NullOrWhiteSpace(postgresSettings.ConnectionString);

        services.AddDbContext<IDatabaseContext, DatabaseContext>(builder =>
        {
            builder
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IReadOnlyDatabaseContext, ReadOnlyDatabaseContext>();

        return services;
    }
}
