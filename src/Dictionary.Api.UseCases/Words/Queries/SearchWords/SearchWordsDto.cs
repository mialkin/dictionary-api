namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

public record SearchWordsDto(
    Guid Id,
    int LanguageId,
    int GenderId,
    string Name,
    string? Transcription,
    WordGender? Gender,
    string? Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
