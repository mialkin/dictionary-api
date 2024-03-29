using Dictionary.Api.UseCases.Words.Commands.CreateWord;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Create;

public static class CreateWordEndpoint
{
    public static void MapCreateWord(this IEndpointRouteBuilder builder, string routePattern)
    {
        builder.MapPost(routePattern, async (
                [FromBody] CreateWordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<CreateWordCommand>();
                var result = await sender.Send(command, cancellationToken);

                return result.IsSuccess
                    ? Results.Ok(result.Value.Adapt<CreateWordResponse>())
                    : Results.BadRequest(result.Error);
            })
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Create word" });
    }
}
