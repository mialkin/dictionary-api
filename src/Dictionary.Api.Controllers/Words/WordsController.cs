using Dictionary.Api.Controllers.Words.Requests;
using Dictionary.Api.Domain;
using Dictionary.Api.UseCases.Words.Commands.CreateWord;
using Dictionary.Api.UseCases.Words.Commands.DeleteWord;
using Dictionary.Api.UseCases.Words.Commands.UpdateWord;
using Dictionary.Api.UseCases.Words.Queries.GetWord;
using Dictionary.Api.UseCases.Words.Queries.SearchWords;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers.Words;

public class WordsController : ApplicationController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateWordRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateWordCommand(
            request.LanguageId,
            request.GenderId,
            request.Name,
            request.Translation,
            request.Transcription);

        await Sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetWordQuery(id), cancellationToken);
        if (result.IsFailure)
            return Error(result.Error);

        return FromMaybe(result.Value, error: Errors.General.NotFound(id));
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateWordRequest request, CancellationToken cancellationToken)
    {
        var unitResult = await Sender.Send(
            request: new UpdateWordCommand(request.Id, request.Name, request.Transcription, request.Translation),
            cancellationToken);

        return FromUnitResult(unitResult);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] Guid id, CancellationToken cancellationToken)
    // TODO Use DeleteWordRequest instead just plain id
    {
        var unitResult = await Sender.Send(new DeleteWordCommand(id), cancellationToken);
        return FromUnitResult(unitResult);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] int languageId,
        [FromQuery] int term,
        CancellationToken cancellationToken)
    {
        // TODO Make Language an entity and validate if languageId > 0
        // TODO Return SuccessResponse with request ID, i.e. trace ID
        var result = await Sender.Send(new SearchWordsQuery(languageId), cancellationToken);
        return Ok(result);
    }
}
