namespace Dictionary.Api.UseCases.Words.Queries.ListWords;

public record ListWordsDto(
    Guid Id,
    int LanguageId,
    int GenderId,
    string Name,
    string? Translation,
    string? Transcription,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
