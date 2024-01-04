using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

internal class GetWordQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    : IRequestHandler<GetWordQuery, Result<Maybe<GetWordDto>, Error>>
{
    public async Task<Result<Maybe<GetWordDto>, Error>> Handle(
        GetWordQuery request,
        CancellationToken cancellationToken)
    {
        var queryable = readOnlyDatabaseContext.Words;

        // TODO Return just Maybe, not Result<Maybe> ?

        var word = await queryable
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (word is null)
            return Maybe<GetWordDto>.None;

        var result = new GetWordDto(
            word.Id,
            word.LanguageId,
            word.Name,
            word.Transcription,
            word.Translation,
            word.CreatedAt,
            word.UpdatedAt);

        return Maybe.From(result);
    }
}
