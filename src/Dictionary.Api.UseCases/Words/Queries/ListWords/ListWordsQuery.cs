using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.ListWords;

public record ListWordsQuery(int LanguageId, string? SearchTerm) : IRequest<IReadOnlyCollection<ListWordsDto>>;
