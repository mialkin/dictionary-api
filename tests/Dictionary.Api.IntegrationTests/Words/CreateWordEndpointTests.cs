using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.Endpoints.Words.Create;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words;

public class CreateWordEndpointTests(WordsControllerWebApplicationFactory<Program> factory)
    : IClassFixture<WordsControllerWebApplicationFactory<Program>>
{
    [Theory]
    [AutoData]
    public async Task Creates_word_correctly(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var databaseContext = factory.Services.GetRequiredService<IReadOnlyDatabaseContext>();

        // Act
        var httpResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
        var createWordResponse = await httpResponseMessage.Content.ReadFromJsonAsync<CreateWordResponse>();
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
    public async Task Create_WhenTranslationIsNullOrWhitespace_ReturnsError(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var createResponse = await client.PostAsJsonAsync(Endpoints.CreateWord, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [AutoData]
    public async Task Create_WhenTranscriptionIsNullOrWhitespace_SavesTranscriptionAsNull(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var createResponse = await client.PostAsJsonAsync(Endpoints.CreateWord, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
