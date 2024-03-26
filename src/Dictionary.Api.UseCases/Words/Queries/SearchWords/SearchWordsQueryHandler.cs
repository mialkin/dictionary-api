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
        // TODO Use Elasticsearch for storing words and translations?
        var queryable = readOnlyDatabaseContext.Words.Where(x => x.LanguageId == request.LanguageId);

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            queryable = queryable
                .Where(x => x.Name.StartsWith(request.Query))
                .OrderBy(x => x.Name);
        }
        else
        {
            queryable = queryable.OrderByDescending(x => x.CreatedAt); // TODO Add database index
        }

        // TODO Implement client pagination with default value set on the client and on the server
        var words = await queryable
            .Select(x => new SearchWordsDto(
                x.Id,
                x.LanguageId,
                x.GenderId,
                x.Name,
                x.Transcription,
                new WordGender(x.GenderMasculine, x.GenderFeminine, x.GenderNeuter),
                x.Translation,
                x.UpdatedAt,
                x.CreatedAt))
            .Take(50) // TODO Move to specification and reuse across different methods?
            .ToListAsync(cancellationToken);

        return words;
    }
}
