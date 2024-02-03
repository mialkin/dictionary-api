using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Queries.SearchWords;

internal class SearchWordsQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    : IRequestHandler<SearchWordsQuery, IReadOnlyCollection<SearchWordsDto>>
{
    public async Task<IReadOnlyCollection<SearchWordsDto>> Handle(
        SearchWordsQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = readOnlyDatabaseContext.Words.Where(x => x.LanguageId == request.LanguageId);
        // TODO Use Elasticsearch for storing words and translations?

        if (!string.IsNullOrWhiteSpace(request.Term))
        {
            queryable = queryable
                .Where(x => x.Name.StartsWith(request.Term))
                .OrderBy(x => x.Name);
        }
        else
        {
            queryable = queryable.OrderByDescending(x => x.CreatedAt); // TODO Add database index
        }

        var words = await queryable
            .Select(x => new SearchWordsDto(
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
