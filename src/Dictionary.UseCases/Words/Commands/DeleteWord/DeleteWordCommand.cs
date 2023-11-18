using MediatR;

namespace Dictionary.UseCases.Words.Commands.DeleteWord;

public record DeleteWordCommand(DeleteWordDto DeleteWordDto) : IRequest;
