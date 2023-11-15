using Dictionary.UseCases.Words.Queries.GetWords;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.UseCases.Configuration;

public static class MediatrConfiguration
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        // TODO Replace GetWordsDto with something else
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<GetWordsDto>());

        return services;
    }
}
