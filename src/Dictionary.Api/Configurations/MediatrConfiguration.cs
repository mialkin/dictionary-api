using Dictionary.Api.UseCases.Words.Commands.CreateWord;

namespace Dictionary.Api.Configurations;

public static class MediatrConfiguration
{
    public static IServiceCollection ConfigureMediatr(this IServiceCollection services)
    {
        // TODO Replace CreateWordCommand with something else
        services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<CreateWordCommand>());

        return services;
    }
}
