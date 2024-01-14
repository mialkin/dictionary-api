using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.Controllers.Words.Requests;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using FluentAssertions;
using Flurl;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words;

public class WordsControllerTests(WordsControllerWebApplicationFactory<Program> factory)
    : IClassFixture<WordsControllerWebApplicationFactory<Program>>
{
    private const string BasePath = "api/words";
    private const string CreateSegment = "create";


    [Theory]
    [AutoData]
    public async Task Create_WhenRequestIsValid_CreatesNewWord(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var createUri = BasePath.AppendPathSegment(CreateSegment);

        // Act
        var createResponse = await client.PostAsJsonAsync(createUri, request);

        // Assert
        createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
