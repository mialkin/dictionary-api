using System.Net;
using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary.Api.Controllers;

[ApiController]
public abstract class ApplicationController : ControllerBase
{
    private ISender? _sender;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected static new IActionResult Ok(object? result = null)
    {
        return new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);
    }

    protected IActionResult NotFound(Error error, string? invalidField = null)
    {
        return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.NotFound);
    }

    protected IActionResult Error(Error error, string? invalidField = null)
    {
        return new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.BadRequest);
    }

    protected IActionResult FromResult<T>(Result<T, Error> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return Error(result.Error);
    }

    protected IActionResult FromMaybe<T>(Maybe<T> maybe, Error? error = null)
    {
        if (maybe.HasNoValue && error is null)
            return NotFound();

        if (maybe.HasNoValue && error is not null)
            return NotFound(error);

        return Ok(maybe.Value);
    }

    protected IActionResult FromUnitResult(UnitResult<Error> result)
    {
        if (result.IsSuccess)
            return Ok();

        return Error(result.Error);
    }
}