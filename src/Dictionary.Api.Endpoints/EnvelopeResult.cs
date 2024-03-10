using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Dictionary.Api.Endpoints;

public sealed class EnvelopeResult(Envelope envelope, HttpStatusCode statusCode) : IActionResult
{
    private readonly int _statusCode = (int)statusCode;

    public Task ExecuteResultAsync(ActionContext context)
    {
        var objectResult = new ObjectResult(envelope) { StatusCode = _statusCode };

        return objectResult.ExecuteResultAsync(context);
    }
}
