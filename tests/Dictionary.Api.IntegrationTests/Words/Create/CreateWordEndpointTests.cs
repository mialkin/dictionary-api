using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.Domain;
using Dictionary.Api.Endpoints.Words.Create;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words.Create;

public class CreateWordEndpointTests(WordEndpointsWebApplicationFactory<Program> factory)
    : IClassFixture<WordEndpointsWebApplicationFactory<Program>>
{
    [Theory]
    [InlineAutoData("ɪg'zɑːmpl træn'skrɪpʃ(ə)n")]
    [InlineAutoData(null)]
    public async Task Saves_word_to_database_correctly(string transcription, string name, string translation)
    {
        // Arrange
        var request = new CreateWordRequest(LanguageId: 1, name, transcription, translation);

        var client = factory.CreateClient();
        var databaseContext = factory.Services.GetRequiredService<IReadOnlyDatabaseContext>();
        var httpResponseMessage = await client.PostAsJsonAsync(IntegrationTests.Endpoints.CreateWord, request);
        var createWordResponse = await httpResponseMessage.Content.ReadFromJsonAsync<CreateWordResponse>();

        // Act
        var word = await databaseContext.Words.SingleAsync(x => x.Id == createWordResponse!.Id);

        // Assert
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

        request.LanguageId.Should().Be(word.LanguageId);
        request.Name.Should().Be(word.Name);
        request.Transcription.Should().Be(word.Transcription);
        request.Translation.Should().Be(word.Translation);
    }

    [Theory]
    [AutoData]
    public async Task Does_not_allow_two_of_the_same_words_in_one_dictionary(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var firstResponseMessage = await client.PostAsJsonAsync(IntegrationTests.Endpoints.CreateWord, request);
        var secondResponseMessage = await client.PostAsJsonAsync(IntegrationTests.Endpoints.CreateWord, request);
        var error = await secondResponseMessage.Content.ReadFromJsonAsync<Error>();

        // Assert
        firstResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        secondResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error.Should().Be(Errors.Word.NameAlreadyExists(request.Name));
    }
}
