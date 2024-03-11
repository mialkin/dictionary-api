using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

public record CreateWordCommand(int LanguageId, string Name, string? Transcription, string Translation)
    : IRequest<UnitResult<Error>>;
