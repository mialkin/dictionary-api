using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.ListWords;

public record ListWordsQuery(int LanguageId) : IRequest<IReadOnlyCollection<ListWordsDto>>;
