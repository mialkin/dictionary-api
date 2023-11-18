using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.UseCases.Words.Queries.ListWords;

internal class ListWordsQueryHandler : IRequestHandler<ListWordsQuery, IReadOnlyCollection<ListWordsDto>>
{
    private readonly IReadOnlyDatabaseContext _readOnlyDatabaseContext;

    public ListWordsQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext) =>
        _readOnlyDatabaseContext = readOnlyDatabaseContext;

    public async Task<IReadOnlyCollection<ListWordsDto>> Handle(
        ListWordsQuery request,
        CancellationToken cancellationToken)
    {
        // TODO Use https://github.com/vkhorikov/CSharpFunctionalExtensions

        var queryable = _readOnlyDatabaseContext.Words;
        // TODO Use Elasticsearch for storing words and translations?

        var words = await queryable
            .Where(x => x.LanguageId == request.LanguageId)
            .Select(x =>
                new ListWordsDto(
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
