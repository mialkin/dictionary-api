namespace Dictionary.UseCases.Words.Queries.GetWords;

public record GetWordsDto(
    int LanguageId,
    string Name,
    string? Transcription,
    string Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
