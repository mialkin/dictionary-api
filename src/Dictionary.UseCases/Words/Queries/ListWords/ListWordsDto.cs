namespace Dictionary.UseCases.Words.Queries.ListWords;

public record ListWordsDto(
    int LanguageId,
    string Name,
    string? Transcription,
    string Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
