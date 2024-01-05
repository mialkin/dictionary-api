namespace Dictionary.Api.Domain;

public static class Errors
{
    public static class Word
    {
        public static Error TranslationIsInvalid() =>
            new(code: "word.translation.is.invalid",
                message: $"Translation must be non empty string less than or " +
                         $"equal to {Constants.Words.TranslationMaxLength} characters");

        public static Error TranscriptionIsInvalid() =>
            new(code: "word.transcription.is.invalid",
                message: $"Transcription must be non empty string less than or " +
                         $"equal to {Constants.Words.TranslationMaxLength} characters");
    }

    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            string forId = id == null ? "" : $" for ID '{id}'";
            return new Error(code: "record.not.found", message: $"Record not found{forId}");
        }

        public static Error InternalServerError(string message)
        {
            return new Error("internal.server.error", message);
        }
    }
}
