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
        // TODO Use https://github.com/vkhorikov/CSharpFunctionalExtensions

        var queryable = readOnlyDatabaseContext.Words;
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
