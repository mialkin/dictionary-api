using Dictionary.Api.Endpoints.Words.Create;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words.Endpoints.Create;

public static class CreateWordTestData
{
    public static CreateWordRequest SingleValidRequest =>
        new(LanguageId: 1, Name: "apple", Transcription: "'æpl", Translation: "1) яблоко");

    public static TheoryData<CreateWordRequest> ValidRequests => new()
    {
        new CreateWordRequest(LanguageId: 1, Name: "apple", Transcription: "'æpl", Translation: "1) яблоко"),
        new CreateWordRequest(LanguageId: 1, Name: "apple", Transcription: null, Translation: "1) яблоко")
    };

    public static TheoryData<CreateWordRequest> InvalidTranscriptions => new()
    {
        new CreateWordRequest(LanguageId: 1, Name: "apple", Transcription: string.Empty, Translation: "1) яблоко"),
        new CreateWordRequest(LanguageId: 1, Name: "apple", Transcription: " ", Translation: "1) яблоко"),
        new CreateWordRequest(LanguageId: 1, Name: "apple", Transcription: "         ", Translation: "1) яблоко")
    };
}
