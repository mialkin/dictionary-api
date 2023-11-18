using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.UseCases.Words.Queries.GetWords;

internal class GetWordsQueryHandler : IRequestHandler<GetWordsQuery, IReadOnlyCollection<GetWordsDto>>
{
    private readonly IReadOnlyDatabaseContext _readOnlyDatabaseContext;

    public GetWordsQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext) =>
        _readOnlyDatabaseContext = readOnlyDatabaseContext;

    public async Task<IReadOnlyCollection<GetWordsDto>> Handle(
        GetWordsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO Use https://github.com/vkhorikov/CSharpFunctionalExtensions

        var queryable = _readOnlyDatabaseContext.Words;
        // TODO Use Elasticsearch for storing words and translations?

        var words = await queryable
            .Where(x => x.LanguageId == request.LanguageId)
            .Select(x =>
                new GetWordsDto(
                    x.LanguageId,
                    x.Name,
                    x.Transcription,
                    x.Translation,
                    x.CreatedAt,
                    x.UpdatedAt)
            )
            .ToListAsync(cancellationToken);

        return words;
    }
}
