namespace Dictionary.Api.Domain;

public static class Errors
{
    public static class Word
    {
        public static Error NameIsInvalid() =>
            new(
                code: "word.name.is.invalid",
                message: $"Word must be non empty string less than or " +
                         $"equal to {Constants.Words.NameMaxLength} characters");

        public static Error NameAlreadyExists(string name) =>
            new(code: "word.name.already.exists", message: $"Word \"{name}\" already exists in the dictionary");

        public static Error TranscriptionIsInvalid() =>
            new(
                code: "word.transcription.is.invalid",
                message: $"Transcription must be non empty string less than or " +
                         $"equal to {Constants.Words.TranscriptionMaxLength} characters");

        public static Error TranslationIsInvalid() =>
            new(
                code: "word.translation.is.invalid",
                message: $"Translation must be non empty string less than or " +
                         $"equal to {Constants.Words.TranslationMaxLength} characters");
    }

    public static class General
    {
        public static Error NotFound(Guid? id = null)
        {
            var forId = id is null ? string.Empty : $" for ID \"{id}\"";
            return new Error(code: "record.not.found", message: $"Record not found{forId}");
        }

        public static Error InternalServerError(string message)
        {
            return new Error("internal.server.error", message);
        }
    }
}
