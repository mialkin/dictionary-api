using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

public record DeleteWordCommand(Guid Id) : IRequest<UnitResult<Error>>;
