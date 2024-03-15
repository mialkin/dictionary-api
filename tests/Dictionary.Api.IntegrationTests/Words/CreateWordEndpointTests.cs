using System.Net;
using System.Net.Http.Json;
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
    [MemberData(nameof(CreateWordTestData.ValidRequests), MemberType = typeof(CreateWordTestData))]
    public async Task Stores_word_in_database_correctly(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();
        var databaseContext = factory.Services.GetRequiredService<IReadOnlyDatabaseContext>();
        var httpResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
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

    [Fact]
    public async Task Does_not_allow_two_of_the_same_words_in_dictionary()
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new CreateWordRequest(
            LanguageId: 1,
            Name: Guid.NewGuid().ToString(),
            Transcription: null,
            Translation: Guid.NewGuid().ToString());

        // Act
        var firstResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
        var secondResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);

        // Assert
        firstResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        secondResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
