namespace Dictionary.Api.Domain.Entities;

public class Word
{
    /// <summary>
    /// Internal constructor for ORM
    /// </summary>
#pragma warning disable CS8618
    internal Word()
#pragma warning restore CS8618
    {
    }

    public Word(Guid id, string name, string translation, string transcription)
    {
        Id = id;
        Name = name;
        Translation = translation;
        Transcription = transcription;
    }

    public Guid Id { get; private set; } // TODO remove?
    public string Name { get; private set; }
    public string Translation { get; private set; }
    public string Transcription { get; private set; }
}
