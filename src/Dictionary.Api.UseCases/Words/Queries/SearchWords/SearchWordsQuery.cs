using MediatR;

namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

public record SearchWordsQuery(int LanguageId) : IRequest<IReadOnlyCollection<SearchWordsDto>>;
