using Dictionary.Api.Domain;
using Dictionary.Api.UseCases.Words.Commands.CreateWord;
using Dictionary.Api.UseCases.Words.Commands.DeleteWord;
using Dictionary.Api.UseCases.Words.Commands.UpdateWord;
using Dictionary.Api.UseCases.Words.Queries.GetWord;
using Dictionary.Api.UseCases.Words.Queries.ListWords;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ApplicationController
{
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateWordDto dto, CancellationToken cancellationToken)
    {
        await Sender.Send(new CreateWordCommand(dto), cancellationToken);
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
    public async Task<IActionResult> Update([FromBody] UpdateWordDto dto, CancellationToken cancellationToken)
    {
        var unitResult = await Sender.Send(
            request: new UpdateWordCommand(dto.Id, dto.Name, dto.Transcription, dto.Translation),
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

    [HttpGet("list")]
    public async Task<IActionResult> List(
        [FromQuery] int languageId,
        [FromQuery] string? term,
        CancellationToken cancellationToken)
    {
        // TODO Return SuccessResponse with request ID, i.e. trace ID
        var result = await Sender.Send(new ListWordsQuery(languageId, term), cancellationToken);
        return Ok(result);
    }
}
