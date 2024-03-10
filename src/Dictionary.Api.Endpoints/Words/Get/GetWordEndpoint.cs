using Dictionary.Api.Domain;
using Dictionary.Api.UseCases.Words.Queries.GetWord;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Get;

public static class GetWordEndpoint
{
    public static void MapGetWord(this IEndpointRouteBuilder builder)
    {
        builder.MapGet("get", async (
                [FromQuery] Guid id,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var result = await sender.Send(new GetWordQuery(id), cancellationToken);

                return result.Value.HasValue
                    ? Results.Ok(result.Value.Value)
                    : Results.NotFound(Errors.General.NotFound());
            })
            .Produces<GetWordDto>()
            .Produces<Error>(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Get word by ID" });
    }
}
