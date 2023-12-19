using Dictionary.Api.UseCases.Words.Queries.ListWords;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.UseCases.Configurations;

public static class MediatrConfiguration
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        // TODO Replace GetWordsDto with something else
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<ListWordsDto>());

        return services;
    }
}
