using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using Dictionary.UseCases.Words.Commands.CreateWord;
using FluentAssertions;
using Flurl;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words;

public class WordsControllerTests : IClassFixture<WordsControllerWebApplicationFactory<Program>>
{
    private readonly WordsControllerWebApplicationFactory<Program> _factory;

    private const string BasePath = "api/words";
    private const string CreateSegment = "create";
    private const string ListSegment = "list";

    public WordsControllerTests(WordsControllerWebApplicationFactory<Program> factory) => _factory = factory;

    [Theory]
    [AutoData]
    public async Task Create_WhenRequestIsValid_CreatesWord(CreateWordDto dto)
    {
        // Arrange
        var client = _factory.CreateClient();
        var url = BasePath.AppendPathSegment(CreateSegment);

        // Act
        var response = await client.PostAsJsonAsync(url, dto);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task List_WhenRequestIsValid_ReturnsOk()
    {
        // Arrange
        var client = _factory.CreateClient();
        var url = BasePath.AppendPathSegment(ListSegment);

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
