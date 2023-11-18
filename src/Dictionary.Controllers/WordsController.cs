using CSharpFunctionalExtensions;
using Dictionary.UseCases.Words.Commands.CreateWord;
using Dictionary.UseCases.Words.Commands.DeleteWord;
using Dictionary.UseCases.Words.Commands.UpdateWord;
using Dictionary.UseCases.Words.Queries.GetWord;
using Dictionary.UseCases.Words.Queries.GetWords;
using Dictionary.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ApiControllerBase
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

        if (result.HasValue)
            return Ok(Envelope.Ok(result.Value));

        return Ok(Envelope.Error("Word not found"));
    }

    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateWordDto dto, CancellationToken cancellationToken)
    {
        await Sender.Send(new UpdateWordCommand(dto), cancellationToken);
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
    public async Task<IReadOnlyCollection<GetWordsDto>> List(
        [FromQuery] int languageId,
        [FromQuery] string? term,
        CancellationToken cancellationToken)
    {
        // TODO Return SuccessResponse with request ID, i.e. trace ID
        var result = await Sender.Send(new GetWordsQuery(languageId, term), cancellationToken);
        return result;
    }
}
