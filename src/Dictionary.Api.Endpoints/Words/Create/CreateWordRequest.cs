namespace Dictionary.Api.Endpoints.Words.Create;

public record CreateWordRequest(int LanguageId, int GenderId, string Name, string Translation, string? Transcription);
