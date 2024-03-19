using Dictionary.Api.UseCases.Words.Commands.UpdateWord;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.OpenApi.Models;

namespace Dictionary.Api.Endpoints.Words.Update;

public static class UpdateWordEndpoint
{
    public static void MapUpdateWord(this IEndpointRouteBuilder builder, string routePattern)
    {
        // TODO Move endpoint names to WordEndpoints class
        builder.MapPatch(routePattern, async (
                [FromBody] UpdateWordRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = request.Adapt<UpdateWordCommand>();
                var result = await sender.Send(command, cancellationToken);

                // TODO Implement something like Result.FromUnitResult(result);
                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(result.Error);
            })
            .WithOpenApi(operation => new OpenApiOperation(operation) { Summary = "Update word" });
    }
}
