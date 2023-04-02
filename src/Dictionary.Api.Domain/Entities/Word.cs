using System;

namespace Dictionary.Api.Domain.Entities;

public class Word
{
    /// <summary>
    /// Internal constructor for ORM
    /// </summary>
    internal Word()
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
