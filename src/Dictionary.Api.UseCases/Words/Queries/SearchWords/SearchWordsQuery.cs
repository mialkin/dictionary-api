using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

public record SearchWordsQuery(int LanguageId, string? Query) : IRequest<IReadOnlyCollection<SearchWordsDto>>;
