using CSharpFunctionalExtensions;
using MediatR;

namespace Dictionary.UseCases.Words.Queries.GetWord;

public record GetWordQuery(int LanguageId, string Name) : IRequest<Result<GetWordDto>>;
