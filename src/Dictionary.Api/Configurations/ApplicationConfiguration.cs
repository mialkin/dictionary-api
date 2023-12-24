using Dictionary.Api.Infrastructure.Implementation.Database;
using Dictionary.Api.UseCases.Configurations;

namespace Dictionary.Api.Configurations;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.ConfigureMediatr();

        var postgresSettings =
            builderConfiguration.GetRequiredSection(nameof(PostgresSettings)).Get<PostgresSettings>();
        services.ConfigureDatabase(postgresSettings);

        return services;
    }
}
