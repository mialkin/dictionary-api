using CSharpFunctionalExtensions;
using Dictionary.Common;

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

    public Word(int languageId, string name, string translation, string? transcription)
    {
        Id = Guid.NewGuid();

        LanguageId = languageId;
        Name = name;
        Translation = translation;
        Transcription = transcription;

        var utcNow = DateTime.UtcNow; // TODO Use ISystemClock
        CreatedAt = utcNow;
        UpdatedAt = utcNow;
    }

    public Guid Id { get; private set; }
    public int LanguageId { get; private set; }
    public string Name { get; private set; }
    public string? Transcription { get; private set; }
    public string Translation { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Result<object, Error> CanUpdate(string translation, string? transcription)
    {
        if (string.IsNullOrWhiteSpace(translation) || translation.Length > Constants.Words.TranslationMaxLength)
            return Errors.Word.TranslationIsInvalid();

        if (!string.IsNullOrEmpty(transcription)
            && (transcription.Trim().Length == 0 || transcription.Length > Constants.Words.TranscriptionMaxLength))
            return Errors.Word.TranscriptionIsInvalid();

        return new object(); // TODO Replace with Result.Empty ?
    }

    public void Update(string translation, string? transcription)
    {
        var result = CanUpdate(translation, transcription);
        if (result.IsFailure)
            throw new InvalidOperationException(); // Pass error returned by CanUpdate

        Translation = translation;
        Transcription = transcription;
        UpdatedAt = DateTime.UtcNow; // TODO Use ISystemClock
    }
}
