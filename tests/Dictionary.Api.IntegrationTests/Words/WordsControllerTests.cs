using System.Net;
using System.Threading.Tasks;
using Dictionary.Api.IntegrationTests.Words.Infrastructure;
using FluentAssertions;
using Flurl;
using Xunit;

namespace Dictionary.Api.IntegrationTests.Words;

public class WordsControllerTests : IClassFixture<WordsControllerWebApplicationFactory<Program>>
{
    private const string BasePath = "api/words";
    private readonly WordsControllerWebApplicationFactory<Program> _factory;

    public WordsControllerTests(WordsControllerWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task List_WhenRequestIsValid_ReturnsOk()
    {
        // Arrange.
        var client = _factory.CreateClient();
        var url = BasePath.AppendPathSegment("list");

        // Act.
        var response = await client.GetAsync(url);

        // Assert.
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
