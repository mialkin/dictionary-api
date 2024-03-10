namespace Dictionary.Api.Endpoints.Words.Update;

public record UpdateWordRequest
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Transcription { get; init; }
    public string Translation { get; init; } = null!;
}
