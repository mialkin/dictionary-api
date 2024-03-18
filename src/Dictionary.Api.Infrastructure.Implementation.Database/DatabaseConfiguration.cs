using Dictionary.Api.Infrastructure.Interfaces.Database;
using EntityFramework.Exceptions.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(
        this IServiceCollection services,
        PostgresSettings? postgresSettings)
    {
        if (string.IsNullOrWhiteSpace(postgresSettings?.ConnectionString))
        {
            throw new InvalidOperationException("PostgreSQL connection string is not set");
        }

        services.AddDbContext<IDatabaseContext, DatabaseContext>(builder =>
        {
            builder
                .UseNpgsql(postgresSettings.ConnectionString)
                .UseExceptionProcessor()
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IReadOnlyDatabaseContext, ReadOnlyDatabaseContext>();

        return services;
    }
}
