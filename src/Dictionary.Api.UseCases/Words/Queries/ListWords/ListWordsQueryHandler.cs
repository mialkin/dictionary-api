using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Queries.ListWords;

internal class ListWordsQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    : IRequestHandler<ListWordsQuery, IReadOnlyCollection<ListWordsDto>>
{
    public async Task<IReadOnlyCollection<ListWordsDto>> Handle(
        ListWordsQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = readOnlyDatabaseContext.Words;
        // TODO Use Elasticsearch for storing words and translations?

        var words = await queryable
            .Where(x => x.LanguageId == request.LanguageId)
            .OrderByDescending(x => x.CreatedAt) // TODO Add database index
            .Select(x =>
                new ListWordsDto(
                    x.Id,
                    x.LanguageId,
                    x.GenderId,
                    x.Name,
                    x.Translation,
                    x.Transcription,
                    x.CreatedAt,
                    x.UpdatedAt)
            )
            .Take(100) // TODO Move to specification and reuse across different methods?
            // TODO Implement client pagination with default value set on the client and on the server
            .ToListAsync(cancellationToken);

        return words;
    }
}
