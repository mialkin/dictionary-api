using Dictionary.Api.Infrastructure.Implementation.Database;

namespace Dictionary.Api.Configurations;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(
        this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.ConfigureMediatr();

        var postgresSettings =
            builderConfiguration.GetRequiredSection(key: nameof(PostgresSettings)).Get<PostgresSettings>();
        services.ConfigureDatabase(postgresSettings);

        return services;
    }
}
