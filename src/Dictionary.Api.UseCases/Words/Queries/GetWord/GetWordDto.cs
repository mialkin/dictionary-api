namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

public record GetWordDto(
    Guid Id,
    int LanguageId,
    int GenderId,
    string Name,
    string? Transcription,
    WordGender? Gender,
    string Translation,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
