using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Queries.GetWord;

internal class GetWordQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext)
    : IRequestHandler<GetWordQuery, Result<GetWordDto, Error>>
{
    public async Task<Result<GetWordDto, Error>> Handle(GetWordQuery request, CancellationToken cancellationToken)
    {
        var queryable = readOnlyDatabaseContext.Words;

        var word = await queryable
            .Where(x => x.LanguageId == request.LanguageId && x.Name == request.Name)
            .SingleOrDefaultAsync(cancellationToken);

        if (word is null)
            return Result.Failure<GetWordDto, Error>(error: Errors.General.NotFound());

        var result = new GetWordDto(
            word.LanguageId,
            word.Name,
            word.Transcription,
            word.Translation,
            word.CreatedAt,
            word.UpdatedAt);

        return result;
    }
}
