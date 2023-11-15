namespace Dictionary.Api.Domain.Entities;

public class Word : AuditableEntity
{
    /// <summary>
    /// Internal constructor for ORM
    /// </summary>
#pragma warning disable CS8618
    internal Word()
#pragma warning restore CS8618
    {
    }

    public Word(int languageId, string name, string translation, string? transcription)
    {
        LanguageId = languageId;
        Name = name;
        Translation = translation;
        Transcription = transcription;
    }

    public int LanguageId { get; private set; }
    public string Name { get; private set; }
    public string Translation { get; private set; }
    public string? Transcription { get; private set; }
}
