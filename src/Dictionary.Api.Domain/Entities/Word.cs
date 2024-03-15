using CSharpFunctionalExtensions;

namespace Dictionary.Api.Domain.Entities;

public class Word
{
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

    public static Word Create(int languageId, string name, string? transcription, string translation)
    {
        var unitResult = CanCreate(name, transcription, translation);
        if (unitResult.IsFailure)
        {
            throw new InvalidOperationException();
        }

        return new Word(languageId, genderId: 0, name, translation, transcription);
    }

    public static UnitResult<Error> CanCreate(string name, string? transcription, string translation)
    {
        if (!NameIsValid(name))
        {
            return Errors.Word.NameIsInvalid();
        }

        if (!string.IsNullOrWhiteSpace(transcription)
            && (transcription.Trim().Length == 0 || transcription.Length > Constants.Words.TranscriptionMaxLength))
        {
            return Errors.Word.TranscriptionIsInvalid();
        }

        if (string.IsNullOrWhiteSpace(translation) || translation.Length > Constants.Words.TranslationMaxLength)
        {
            return Errors.Word.TranslationIsInvalid();
        }

        return UnitResult.Success<Error>();
    }

    public static UnitResult<Error> CanUpdate(string? transcription, string translation)
    {
        if (!string.IsNullOrWhiteSpace(transcription)
            && (transcription.Trim().Length == 0 || transcription.Length > Constants.Words.TranscriptionMaxLength))
        {
            return Errors.Word.TranscriptionIsInvalid();
        }

        // TODO Make translation a value object and move length check inside of it?
        if (string.IsNullOrWhiteSpace(translation) || translation.Length > Constants.Words.TranslationMaxLength)
        {
            return Errors.Word.TranslationIsInvalid();
        }

        return UnitResult.Success<Error>();
    }

    public void Update(string? transcription, string translation)
    {
        var unitResult = CanUpdate(transcription, translation);
        if (unitResult.IsFailure)
        {
            throw new InvalidOperationException(); // Pass error returned by CanUpdate and word ID
        }

        Transcription = transcription;
        Translation = translation;
        UpdatedAt = DateTime.UtcNow; // TODO Use ISystemClock
    }

    /// <summary>
    /// Internal constructor for ORM.
    /// </summary>
#pragma warning disable CS8618
    internal Word()
#pragma warning restore CS8618
    {
    }

    private static bool NameIsValid(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && name.Length <= Constants.Words.NameMaxLength;
    }
}
