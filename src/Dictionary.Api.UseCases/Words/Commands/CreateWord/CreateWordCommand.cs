using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

public record CreateWordCommand(int LanguageId, int GenderId, string Name, string Translation, string? Transcription)
    : IRequest<UnitResult<Error>>;
