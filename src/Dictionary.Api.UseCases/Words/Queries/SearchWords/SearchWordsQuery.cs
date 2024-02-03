using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

public record SearchWordsQuery(int LanguageId, string? Term) : IRequest<IReadOnlyCollection<SearchWordsDto>>;
