namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

public record CreateWordDto(int LanguageId, string Name, string? Transcription, string Translation);