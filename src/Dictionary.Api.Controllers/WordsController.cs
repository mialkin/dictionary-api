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
    public async Task<IActionResult> Get(
        [FromQuery] int languageId,
        [FromQuery] string name,
        CancellationToken cancellationToken)
    {
        var result = await Sender.Send(new GetWordQuery(languageId, name), cancellationToken);
        return FromResult(result);
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateWordDto dto, CancellationToken cancellationToken)
    {
        await Sender.Send(
            request: new UpdateWordCommand(dto.Id, dto.Name, dto.Transcription, dto.Translation),
            cancellationToken);

        return Ok();
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> Delete([FromBody] DeleteWordDto dto, CancellationToken cancellationToken)
    {
        await Sender.Send(new DeleteWordCommand(dto), cancellationToken);

        // TODO Return different responses based on outcomes of operations
        // TODO Use our Result plus take a look at https://enterprisecraftsmanship.com/posts/functional-c-handling-failures-input-errors/
        return Ok();
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
