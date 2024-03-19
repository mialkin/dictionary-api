using CSharpFunctionalExtensions;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

internal class GetWordQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    : IRequestHandler<GetWordQuery, Maybe<GetWordDto>>
{
    public async Task<Maybe<GetWordDto>> Handle(
        GetWordQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = readOnlyDatabaseContext.Words;

        var word = await queryable
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (word is null)
        {
            return Maybe<GetWordDto>.None;
        }

        var result = new GetWordDto(
            word.Id,
            word.LanguageId,
            word.GenderId,
            word.Name,
            word.Translation,
            word.Transcription,
            word.CreatedAt,
            word.UpdatedAt);

        return Maybe.From(result);
    }
}
