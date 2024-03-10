using Dictionary.Api.Endpoints.Words.Requests;
using Dictionary.Api.UseCases.Words.Commands.CreateWord;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Endpoints.Words;

public static class CreateWordsEndpoint
{
    public static void MapCreateWords(this IEndpointRouteBuilder builder)
    {
        builder.MapPost("create", async (
            [FromBody] CreateWordRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateWordCommand(
                request.LanguageId,
                request.GenderId,
                request.Name,
                request.Translation,
                request.Transcription);

            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        });
    }
}
