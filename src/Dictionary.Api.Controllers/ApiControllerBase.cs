using CSharpFunctionalExtensions;
using Dictionary.Api.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Controllers;

[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected new IActionResult Ok() => base.Ok(Envelope.Ok());

    protected IActionResult Ok<T>(T result) => base.Ok(Envelope.Ok(result));

    protected IActionResult FromResult(Result result)
    {
        if (result.IsSuccess)
            return Ok();

        // if (result.Error == Errors.General.NotFound())
        //     return NotFound(Envelope.Error(result.Error));

        // TODO Implement https://enterprisecraftsmanship.com/posts/advanced-error-handling-techniques/

        return BadRequest(Envelope.Error(result.Error));
    }
}
