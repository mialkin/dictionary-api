using Dictionary.UseCases.Words.Commands.CreateWord;
using Dictionary.UseCases.Words.Commands.DeleteWord;
using Dictionary.UseCases.Words.Commands.UpdateWord;
using Dictionary.UseCases.Words.Queries.GetWords;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ApiControllerBase
{
    [HttpGet("list")]
    public async Task<IActionResult> List([FromQuery] int languageId, string? term, CancellationToken cancellationToken)
    {
        // TODO Return SuccessResponse with request ID, i.e. trace ID
        var result = await Sender.Send(new GetWordsQuery(languageId, term), cancellationToken);

        return Ok(result);
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateWordDto dto, CancellationToken cancellationToken)
    {
        await Sender.Send(new CreateWordCommand(dto), cancellationToken);

        return Ok();
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
        return Ok();
    }
}
