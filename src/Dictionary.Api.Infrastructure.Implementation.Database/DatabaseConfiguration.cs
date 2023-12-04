using Dictionary.Api.Infrastructure.Interfaces.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Infrastructure.Implementation.Database;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        // TODO Move to appsettings.Ide.json
        var connectionString =
            "User ID=dictionary_api;Password=dictionary_api;Host=localhost;Port=2200;Database=dictionary_api;Pooling=true";

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
