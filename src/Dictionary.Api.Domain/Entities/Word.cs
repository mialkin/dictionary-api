﻿namespace Dictionary.Api.Domain.Entities;

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

    public Word(int languageId, string name, string? transcription, string translation)
    {
        LanguageId = languageId;
        Name = name;
        Transcription = transcription;
        Translation = translation;
    }

    public int LanguageId { get; private set; }
    public string Name { get; private set; }
    public string? Transcription { get; private set; }
    public string Translation { get; private set; }

    public void Update(string? transcription, string translation)
    {
        Transcription = transcription;
        Translation = translation;
    }
}
