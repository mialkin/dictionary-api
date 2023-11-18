using Dictionary.UseCases.Words.Commands.CreateWord;
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
    public async Task<IActionResult> Create([FromBody] CreateWordDto createWordDto, CancellationToken cancellationToken)
    {
        await Sender.Send(new CreateWordCommand(createWordDto), cancellationToken);

        return Ok();
    }

    [HttpPut("update")]
    public IActionResult Update()
    {
        return Ok("Word updated " + DateTime.Now);
    }

    [HttpDelete("delete")]
    public IActionResult Delete()
    {
        return Ok("Word deleted " + DateTime.Now);
    }
}
