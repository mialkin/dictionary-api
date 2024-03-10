using Dictionary.Api.Controllers.Words.Requests;
using Dictionary.Api.UseCases.Words.Commands.UpdateWord;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Controllers.Words;

public static class UpdateWordEndpoint
{
    public static void MapUpdateWords(this IEndpointRouteBuilder builder)
    {
        builder.MapPatch("update", async (
            [FromBody] UpdateWordRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(
                request: new UpdateWordCommand(request.Id, request.Name, request.Transcription, request.Translation),
                cancellationToken);

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        });
    }
}
