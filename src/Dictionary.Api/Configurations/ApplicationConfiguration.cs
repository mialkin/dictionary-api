using Dictionary.Api.Infrastructure.Implementation.Database;
using Dictionary.Api.UseCases.Configurations;

namespace Dictionary.Api.Configurations;

public static class ApplicationConfiguration
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services)
    {
        services.ConfigureMediatr();
        services.ConfigureDatabase();

        return services;
    }
}
