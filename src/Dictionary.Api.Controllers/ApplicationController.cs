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

    protected static new IActionResult Ok(object? result = null) =>
        new EnvelopeResult(Envelope.Ok(result), HttpStatusCode.OK);

    protected static IActionResult NotFound(Error? error, string? invalidField = null) =>
        new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.NotFound);

    protected static IActionResult Error(Error error, string? invalidField = null) =>
        new EnvelopeResult(Envelope.Error(error, invalidField), HttpStatusCode.BadRequest);

    protected IActionResult FromResult<T>(Result<T, Error> result) =>
        result.IsSuccess ? Ok(result.Value) : Error(result.Error);

    protected static IActionResult FromMaybe<T>(Maybe<T> maybe, Error? error = null) =>
        maybe.HasNoValue ? NotFound(error) : Ok(maybe.Value);

    protected static IActionResult FromUnitResult(UnitResult<Error> result) =>
        result.IsSuccess ? Ok() : Error(result.Error);
}
