using MediatR;

namespace Dictionary.UseCases.Words.Queries.ListWords;

public record ListWordsQuery(int LanguageId, string? SearchTerm) : IRequest<IReadOnlyCollection<ListWordsDto>>;
