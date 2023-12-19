using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

public record CreateWordCommand(CreateWordDto CreateWordDto) : IRequest;
