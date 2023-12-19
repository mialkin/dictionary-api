namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordDto(
    int LanguageId,
    string Name,
    string? Transcription,
    string Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
