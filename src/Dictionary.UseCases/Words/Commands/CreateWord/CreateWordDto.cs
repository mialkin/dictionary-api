namespace Dictionary.UseCases.Words.Commands.CreateWord;

public record CreateWordDto(int LanguageId, string Name, string? Transcription, string Translation);
