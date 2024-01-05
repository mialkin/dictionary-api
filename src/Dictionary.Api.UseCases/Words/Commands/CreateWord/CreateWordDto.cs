namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

public record CreateWordDto(int LanguageId, int GenderId, string Name, string Translation, string? Transcription);
