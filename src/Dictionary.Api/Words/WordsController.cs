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

    [HttpPost("create")]
    public IActionResult Create()
    {
        return Ok("Word created " + DateTime.Now);
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
