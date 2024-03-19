using Dictionary.Api.UseCases.Words.Commands.DeleteWord;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Delete;

public static class DeleteWordEndpoint
{
    public static void MapDeleteWord(this IEndpointRouteBuilder builder, string routePattern)
    {
        builder.MapDelete(routePattern, async (
                [FromBody] DeleteWordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<DeleteWordCommand>();
                await sender.Send(command, cancellationToken);

                return Results.Ok();
            })
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Delete word by ID" });
    }
}
