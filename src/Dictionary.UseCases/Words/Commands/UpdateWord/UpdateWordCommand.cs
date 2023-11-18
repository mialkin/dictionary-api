using MediatR;

namespace Dictionary.UseCases.Words.Commands.UpdateWord;

public record UpdateWordCommand(UpdateWordDto UpdateWordDto) : IRequest;
