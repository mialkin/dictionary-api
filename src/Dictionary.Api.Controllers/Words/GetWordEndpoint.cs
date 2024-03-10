using Dictionary.Api.Domain;
using Dictionary.Api.UseCases.Words.Queries.GetWord;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Controllers.Words;

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
            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return result.Value.HasNoValue
                ? Results.NotFound(Errors.General.NotFound())
                : Results.Ok(Envelope.Ok(result.Value.Value));
        });
    }
}
