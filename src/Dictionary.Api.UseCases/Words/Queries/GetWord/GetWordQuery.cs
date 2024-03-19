using CSharpFunctionalExtensions;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordQuery(Guid Id) : IRequest<Maybe<GetWordDto>>;
