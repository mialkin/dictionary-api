namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordDto(
    Guid Id,
    int LanguageId,
    int GenderId,
    string Name,
    string? Translation,
    string? Transcription,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
