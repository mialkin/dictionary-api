namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordDto(
    Guid Id,
    int LanguageId,
    string Name,
    string? Transcription,
    string Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
