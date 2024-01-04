namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public record UpdateWordDto // Rename to UpdateWordRequest and put along with controllers as well as other DTOs inside project
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string? Transcription { get; init; }
    public string Translation { get; init; } = null!;
}
