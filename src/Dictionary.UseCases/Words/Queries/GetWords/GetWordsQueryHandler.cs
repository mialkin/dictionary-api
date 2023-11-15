using MediatR;

namespace Dictionary.UseCases.Words.Queries.GetWords;

internal class GetWordsQueryHandler : IRequestHandler<GetWordsQuery, IReadOnlyCollection<GetWordsDto>>
{
    public async Task<IReadOnlyCollection<GetWordsDto>> Handle(GetWordsQuery request,
        CancellationToken cancellationToken)
    {
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
