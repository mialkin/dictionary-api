using Dictionary.Api.UseCases.Words.Queries.SearchWords;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Search;

public static class SearchWordsEndpoint
{
    public static void MapSearchWords(this IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("search", async (
                [AsParameters] SearchWordsRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                // TODO Make Language an entity and validate if languageId > 0
                // TODO Return SuccessResponse with request ID, i.e. trace ID
                var query = request.Adapt<SearchWordsQuery>();
                var result = await sender.Send(query, cancellationToken);
                return Results.Ok(result);
            })
            .Produces<IReadOnlyCollection<SearchWordsDto>>()
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Get words by filter" });
    }
}
