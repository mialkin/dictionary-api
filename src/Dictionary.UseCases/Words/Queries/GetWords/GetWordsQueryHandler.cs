using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.UseCases.Words.Queries.GetWords;

internal class GetWordsQueryHandler : IRequestHandler<GetWordsQuery, IReadOnlyCollection<GetWordsDto>>
{
    private readonly IReadOnlyDatabaseContext _readOnlyDatabaseContext;

    public GetWordsQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    {
        _readOnlyDatabaseContext = readOnlyDatabaseContext;
    }

    public async Task<IReadOnlyCollection<GetWordsDto>> Handle(
        GetWordsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO Use https://github.com/vkhorikov/CSharpFunctionalExtensions

        var allWords = _readOnlyDatabaseContext.Words.ToList();

        await Task.Yield();

        var words = new List<GetWordsDto>
        {
            new GetWordsDto(
                LanguageId: 1,
                Name: "weird",
                Transcription: null,
                Translation: "странный",
                Gender: null,
                DateTime.UtcNow,
                UpdatedAt: null)
        };

        return words;
    }
}
