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
                var maybe = await sender.Send(query, cancellationToken);

                return maybe.HasValue
                    ? Results.Ok(maybe.Value)
                    : Results.BadRequest(Errors.General.NotFound(request.Id));
            })
            .Produces<GetWordDto>()
            .Produces<Error>(StatusCodes.Status400BadRequest)
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Get word by ID" });
    }
}
