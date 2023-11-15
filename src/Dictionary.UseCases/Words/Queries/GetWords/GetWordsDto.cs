namespace Dictionary.UseCases.Words.Queries.GetWords;

public record GetWordsDto(
    int LanguageId,
    string Name,
    string? Transcription,
    string Translation,
    string? Gender,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
