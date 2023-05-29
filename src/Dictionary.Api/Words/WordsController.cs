using System;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Words;

[ApiController]
[Route("api/[controller]")]
public class WordsController : ControllerBase
{
    [HttpGet("list")]
    public IActionResult List()
    {
        return Ok("List of words " + DateTime.Now);
    }

    [HttpDelete("delete")]
    public IActionResult Delete()
    {
        return Ok("Word deleted " + DateTime.Now);
    }
}
