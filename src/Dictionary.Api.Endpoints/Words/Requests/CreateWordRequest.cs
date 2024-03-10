namespace Dictionary.Api.Endpoints.Words.Requests;

public record CreateWordRequest(int LanguageId, int GenderId, string Name, string Translation, string? Transcription);
