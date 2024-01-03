using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public record UpdateWordCommand(Guid Id, string Name, string? Transcription, string Translation) : IRequest;
