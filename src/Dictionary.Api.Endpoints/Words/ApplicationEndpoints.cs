using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Endpoints.Words;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this IEndpointRouteBuilder builder)
    {
        var wordsGroupBuilder = builder.MapGroup("api/words").RequireAuthorization();
        wordsGroupBuilder.MapSearchWords();
        wordsGroupBuilder.MapCreateWords();
        wordsGroupBuilder.MapGetWord();
        wordsGroupBuilder.MapUpdateWords();
        wordsGroupBuilder.MapDeleteWord();
    }
}
