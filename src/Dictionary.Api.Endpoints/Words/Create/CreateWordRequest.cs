namespace Dictionary.Api.Endpoints.Words.Create;

public record CreateWordRequest(int LanguageId, string Name, string? Transcription, string Translation);
