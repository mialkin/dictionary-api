namespace Dictionary.Api.Endpoints.Words.Search;

public record SearchWordsRequest(int LanguageId, string? Query);
