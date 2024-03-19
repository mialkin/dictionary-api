using Dictionary.Api.Domain;
using Dictionary.Api.UseCases.Words.Queries.GetWord;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Get;

public static class GetWordEndpoint
{
    public static void MapGetWord(this IEndpointRouteBuilder builder, string routePattern)
    {
        builder.MapGet(routePattern, async (
                [AsParameters] GetWordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var query = request.Adapt<GetWordQuery>();
                var result = await sender.Send(query, cancellationToken);

                return result.Value.HasValue
                    ? Results.Ok(result.Value.Value)
                    : Results.NotFound(Errors.General.NotFound());
            })
            .Produces<GetWordDto>()
            .Produces<Error>(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Get word by ID" });
    }
}
