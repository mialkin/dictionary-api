using System.Net;
using System.Net.Http.Json;
using AutoFixture.Xunit2;
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
    [InlineAutoData("ɪg'zɑːmpl")]
    [InlineAutoData(null)]
    public async Task Save_correct_word(string transcription, string name, string translation)
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new CreateWordRequest(LanguageId: 1, name, transcription, translation);
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
    [InlineAutoData(null)]
    [InlineAutoData("")]
    [InlineAutoData(" ")]
    public async Task Do_not_save_word_with_incorrect_name(string name, string transcription, string translation)
    {
        // Arrange
        var client = factory.CreateClient();
        var request = new CreateWordRequest(LanguageId: 1, name, transcription, translation);

        // Act
        var httpResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
        var createWordResponse = await httpResponseMessage.Content.ReadFromJsonAsync<ApiError>();

        // Assert
        httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        createWordResponse!.Code.Should().Be("word.name.is.invalid");
    }

    [Theory]
    [AutoData]
    public async Task Do_not_allow_to_save_two_of_the_same_words_in_the_same_dictionary(CreateWordRequest request)
    {
        // Arrange
        var client = factory.CreateClient();

        // Act
        var firstResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
        var secondResponseMessage = await client.PostAsJsonAsync(Endpoints.CreateWord, request);
        var error = await secondResponseMessage.Content.ReadFromJsonAsync<ApiError>();

        // Assert
        firstResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);
        secondResponseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        error!.Code.Should().Be("word.name.already.exists");
    }
}
