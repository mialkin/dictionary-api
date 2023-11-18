using CSharpFunctionalExtensions;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.UseCases.Words.Queries.GetWord;

internal class GetWordQueryHandler : IRequestHandler<GetWordQuery, Result<GetWordDto>>
{
    private readonly IReadOnlyDatabaseContext _readOnlyDatabaseContext;

    public GetWordQueryHandler(IReadOnlyDatabaseContext readOnlyDatabaseContext) =>
        _readOnlyDatabaseContext = readOnlyDatabaseContext;

    public async Task<Result<GetWordDto>> Handle(GetWordQuery request, CancellationToken cancellationToken)
    {
        var queryable = _readOnlyDatabaseContext.Words;

        var word = await queryable
            .Where(x => x.LanguageId == request.LanguageId && x.Name == request.Name)
            .SingleOrDefaultAsync(cancellationToken);

        if (word is null)
            return Result.Failure<GetWordDto>("Word not found");

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
