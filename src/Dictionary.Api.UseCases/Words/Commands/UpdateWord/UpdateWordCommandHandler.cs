using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using Mapster;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

internal class UpdateWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<UpdateWordCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(UpdateWordCommand request, CancellationToken cancellationToken)
    {
        var word = await databaseContext.Words.FindAsync(request.Id, cancellationToken);
        if (word is null)
        {
            return UnitResult.Failure(Errors.General.NotFound());
        }

        var unitResult = Word.CanUpdate(request.Transcription, request.Translation);
        if (unitResult.IsFailure)
        {
            return unitResult.Error;
        }

        word.Update(request.Transcription, request.Gender.Adapt<Domain.ValueObjects.WordGender>(), request.Translation);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<Error>();
    }
}
