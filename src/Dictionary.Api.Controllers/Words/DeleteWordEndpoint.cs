using Dictionary.Api.Controllers.Words.Requests;
using Dictionary.Api.UseCases.Words.Commands.DeleteWord;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Dictionary.Api.Controllers.Words;

public static class DeleteWordEndpoint
{
    public static void MapDeleteWord(this IEndpointRouteBuilder builder)
    {
        builder.MapDelete("delete", async (
            [FromBody] DeleteWordRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            var result = await sender.Send(new DeleteWordCommand(request.Id), cancellationToken);

            return result.IsSuccess
                ? Results.Ok()
                : Results.BadRequest(result.Error);
        });
    }
}
