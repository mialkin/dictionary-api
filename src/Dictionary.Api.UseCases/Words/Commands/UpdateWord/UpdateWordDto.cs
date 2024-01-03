namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public record UpdateWordDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Transcription { get; init; }
    public string Translation { get; init; } = null!;
}
