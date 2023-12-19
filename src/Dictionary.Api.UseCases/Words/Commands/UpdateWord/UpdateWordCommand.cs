using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public record UpdateWordCommand(UpdateWordDto UpdateWordDto) : IRequest;
