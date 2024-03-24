using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public record UpdateWordCommand(Guid Id, string Name, string? Transcription, WordGender? Gender, string Translation)
    : IRequest<UnitResult<Error>>;
