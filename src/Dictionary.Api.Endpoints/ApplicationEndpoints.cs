using Dictionary.Api.Endpoints.Words;
using Dictionary.Api.Endpoints.Words.Create;
using Dictionary.Api.Endpoints.Words.Delete;
using Dictionary.Api.Endpoints.Words.Get;
using Dictionary.Api.Endpoints.Words.Search;
using Dictionary.Api.Endpoints.Words.Update;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Endpoints;

public static class ApplicationEndpoints
{
    public static void MapApplicationEndpoints(this IEndpointRouteBuilder builder)
    {
        var wordsGroupBuilder = builder.MapGroup("api/words").RequireAuthorization();
        wordsGroupBuilder.MapSearchWords();
        wordsGroupBuilder.MapCreateWord();
        wordsGroupBuilder.MapGetWord();
        wordsGroupBuilder.MapUpdateWords();
        wordsGroupBuilder.MapDeleteWord();
    }
}
