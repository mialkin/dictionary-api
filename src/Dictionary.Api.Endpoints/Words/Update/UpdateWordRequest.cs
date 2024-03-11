namespace Dictionary.Api.Endpoints.Words.Update;

public record UpdateWordRequest(Guid Id, string Name, string? Transcription, string Translation);
