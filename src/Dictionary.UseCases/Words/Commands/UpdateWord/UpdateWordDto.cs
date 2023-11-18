namespace Dictionary.UseCases.Words.Commands.UpdateWord;

public record UpdateWordDto
{
    public int LanguageId { get; init; } // TODO Remove init?
    public string Name { get; init; } = null!;
    public string? Transcription { get; init; }
    public string Translation { get; init; } = null!;

    public object[] GetPrimaryKey() => new object[] { LanguageId, Name };
}
