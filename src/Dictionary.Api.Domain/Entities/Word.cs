using CSharpFunctionalExtensions;
using Dictionary.Api.Domain.ValueObjects;

namespace Dictionary.Api.Domain.Entities;

public class Word
{
    public Word(
        int languageId,
        int genderId,
        string name,
        string? transcription,
        WordGender? gender,
        string? translation)
    {
        Id = Guid.NewGuid();
        LanguageId = languageId;
        GenderId = genderId;
        Name = name;
        Transcription = transcription;

        if (gender is not null)
        {
            GenderMasculine = gender.Masculine;
            GenderFeminine = gender.Feminine;
            GenderNeuter = gender.Neuter;
        }

        Translation = translation;

        var utcNow = DateTime.UtcNow; // TODO Use ISystemClock
        CreatedAt = utcNow;
        UpdatedAt = utcNow;
    }

    public Guid Id { get; private set; }

    public int LanguageId { get; private set; }

    public int GenderId { get; private set; }

    public string Name { get; private set; }

    public string? Transcription { get; private set; }

    public string? Translation { get; private set; }

    public bool GenderMasculine { get; private set; }

    public bool GenderFeminine { get; private set; }

    public bool GenderNeuter { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime UpdatedAt { get; private set; }

    public static Word Create(
        int languageId,
        string name,
        string? transcription,
        WordGender? gender,
        string translation)
    {
        var unitResult = CanCreate(languageId, name, transcription, translation);
        if (unitResult.IsFailure)
        {
            throw new InvalidOperationException();
        }

        return new Word(
            languageId,
            genderId: 0,
            name: name.Trim(),
            transcription: string.IsNullOrWhiteSpace(transcription) ? null : transcription.Trim(),
            gender,
            translation: translation.Trim());
    }

    public static UnitResult<Error> CanCreate(int? languageId, string? name, string? transcription, string? translation)
    {
        if (languageId is null)
        {
            return Errors.Word.LanguageIdIsInvalid(languageId);
        }

        if (!IsNameValid(name))
        {
            return Errors.Word.NameIsInvalid();
        }

        // TODO Use value object for transcription. Encapsulate all the checks inside Transcription class
        if (!IsTranscriptionValid(transcription))
        {
            return Errors.Word.TranscriptionIsInvalid();
        }

        if (!IsTranslationValid(translation))
        {
            return Errors.Word.TranslationIsInvalid();
        }

        return UnitResult.Success<Error>();
    }

    public static UnitResult<Error> CanUpdate(string? transcription, string? translation)
    {
        if (!IsTranscriptionValid(transcription))
        {
            return Errors.Word.TranscriptionIsInvalid();
        }

        if (!IsTranslationValid(translation))
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

        Transcription = string.IsNullOrWhiteSpace(transcription) ? null : transcription.Trim();
        Translation = translation.Trim();
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

    private static bool IsNameValid(string? name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return false;
        }

        return name.Trim().Length <= Constants.Words.NameMaxLength;
    }

    private static bool IsTranscriptionValid(string? transcription)
    {
        if (string.IsNullOrWhiteSpace(transcription))
        {
            return true;
        }

        return transcription.Trim().Length <= Constants.Words.TranscriptionMaxLength;
    }

    private static bool IsTranslationValid(string? translation)
    {
        if (string.IsNullOrWhiteSpace(translation))
        {
            return false;
        }

        return translation.Trim().Length <= Constants.Words.TranslationMaxLength;
    }
}
