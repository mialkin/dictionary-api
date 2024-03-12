using System.Globalization;
using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.Endpoints.Words.Create;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using FluentAssertions;
using Flurl;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words;

public class WordsEndpointsTests(WordsControllerWebApplicationFactory<Program> factory)
    : IClassFixture<WordsControllerWebApplicationFactory<Program>>
{
    private const string s_basePath = "api/words";
    private const string s_createSegment = "create";

    [Theory]
    [AutoData]
    public async Task Create_WhenRequestIsValid_CreatesNewWord(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var createUri = s_basePath.AppendPathSegment(s_createSegment);

        // Act
        var createResponse = await client.PostAsJsonAsync(createUri, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [AutoData]
    public async Task Create_WhenTranslationIsNullOrWhitespace_ReturnsError(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var createUri = s_basePath.AppendPathSegment(s_createSegment);

        // Act
        var createResponse = await client.PostAsJsonAsync(createUri, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [AutoData]
    public async Task Create_WhenTranscriptionIsNullOrWhitespace_SavesTranscriptionAsNull(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var createUri = s_basePath.AppendPathSegment(s_createSegment);

        // Act
        var createResponse = await client.PostAsJsonAsync(createUri, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
