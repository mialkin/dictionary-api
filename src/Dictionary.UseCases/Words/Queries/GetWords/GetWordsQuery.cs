using MediatR;

namespace Dictionary.UseCases.Words.Queries.GetWords;

public record GetWordsQuery(int LanguageId, string? SearchTerm) : IRequest;
