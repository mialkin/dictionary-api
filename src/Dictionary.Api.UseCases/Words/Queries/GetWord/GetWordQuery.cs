using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordQuery(int LanguageId, string Name) : IRequest<Result<GetWordDto, Error>>;
