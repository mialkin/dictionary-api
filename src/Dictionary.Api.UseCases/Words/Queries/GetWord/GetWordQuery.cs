using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordQuery(Guid Id) : IRequest<Result<Maybe<GetWordDto>, Error>>;
