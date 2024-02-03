namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

public record SearchWordsDto(
    Guid Id,
    int LanguageId,
    int GenderId,
    string Name,
    string? Translation,
    string? Transcription,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
