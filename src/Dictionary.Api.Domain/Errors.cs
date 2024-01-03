using Dictionary.Common;

namespace Dictionary.Api.Domain;

public static class Errors
{
    public static class Word
    {
        public static Error TranslationIsInvalid() =>
            new(code: "word.translation.is.invalid",
                message: $"Translation must be non empty string less than or " +
                         $"equal to {Constants.Words.TranslationMaxLength} characters in length");

        public static Error TranscriptionIsInvalid() =>
            new(code: "word.transcription.is.invalid",
                message: $"Transcription must be non empty string less than or " +
                         $"equal to {Constants.Words.TranslationMaxLength} characters in length");
    }
}
