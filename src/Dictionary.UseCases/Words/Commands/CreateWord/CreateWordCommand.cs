using MediatR;

namespace Dictionary.UseCases.Words.Commands.CreateWord;

public record CreateWordCommand(CreateWordDto CreateWordDto) : IRequest;
