using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public class UpdateWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<UpdateWordCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(UpdateWordCommand request, CancellationToken cancellationToken)
    {
        var word = await databaseContext.Words.FindAsync(request.Id, cancellationToken);

        if (word is null)
            return UnitResult.Failure(Errors.General.NotFound());

        var unitResult = Word.CanUpdate(request.Translation, request.Transcription);
        if (unitResult.IsFailure)
            return unitResult.Error;

        word.Update(request.Translation, request.Transcription);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<Error>();
    }
}
