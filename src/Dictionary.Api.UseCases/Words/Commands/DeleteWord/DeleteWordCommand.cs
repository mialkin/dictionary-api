using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

public record DeleteWordCommand(DeleteWordDto DeleteWordDto) : IRequest;
