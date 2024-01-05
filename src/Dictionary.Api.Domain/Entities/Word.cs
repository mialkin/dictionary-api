using CSharpFunctionalExtensions;

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

    public Word(int languageId, int genderId, string name, string? translation, string? transcription)
    {
        Id = Guid.NewGuid();
        LanguageId = languageId;
        GenderId = genderId;
        Name = name;
        Translation = translation;
        Transcription = transcription;

        var utcNow = DateTime.UtcNow; // TODO Use ISystemClock
        CreatedAt = utcNow;
        UpdatedAt = utcNow;
    }

    public Guid Id { get; private set; }
    public int LanguageId { get; private set; }
    public int GenderId { get; private set; }
    public string Name { get; private set; }
    public string? Translation { get; private set; }
    public string? Transcription { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public static UnitResult<Error> CanUpdate(string translation, string? transcription)
    {
        if (string.IsNullOrWhiteSpace(translation) || translation.Length > Constants.Words.TranslationMaxLength)
            return Errors.Word.TranslationIsInvalid();

        // TODO Make translation a value object and move length check inside of it?

        if (!string.IsNullOrEmpty(transcription)
            && (transcription.Trim().Length == 0 || transcription.Length > Constants.Words.TranscriptionMaxLength))
            return Errors.Word.TranscriptionIsInvalid();

        return UnitResult.Success<Error>();
    }

    public void Update(string translation, string? transcription)
    {
        var unitResult = CanUpdate(translation, transcription);
        if (unitResult.IsFailure)
            throw new InvalidOperationException(); // Pass error returned by CanUpdate and word ID

        Translation = translation;
        Transcription = transcription;
        UpdatedAt = DateTime.UtcNow; // TODO Use ISystemClock
    }
}
