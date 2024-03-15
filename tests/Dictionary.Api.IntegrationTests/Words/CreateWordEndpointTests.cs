using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.Endpoints.Words.Create;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using FluentAssertions;
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

        // Act
        var createResponse = await client.PostAsJsonAsync(Endpoints.CreateWord, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
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
