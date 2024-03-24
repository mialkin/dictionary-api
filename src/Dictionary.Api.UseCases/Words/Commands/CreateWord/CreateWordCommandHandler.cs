using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using EntityFramework.Exceptions.Common;
using Mapster;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

internal class CreateWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<CreateWordCommand, Result<CreateWordDto, Error>>
{
    public async Task<Result<CreateWordDto, Error>> Handle(
        CreateWordCommand request,
        CancellationToken cancellationToken)
    {
        var unitResult = Word.CanCreate(request.LanguageId, request.Name, request.Transcription, request.Translation);
        if (unitResult.IsFailure)
        {
            return unitResult.Error;
        }

        var word = Word.Create(
            request.LanguageId,
            request.Name,
            request.Transcription,
            request.Gender.Adapt<Domain.ValueObjects.WordGender>(),
            request.Translation);

        databaseContext.Words.Add(word);

        try
        {
            await databaseContext.SaveChangesAsync(cancellationToken);
        }
        catch (UniqueConstraintException exception)
        {
            if (exception.ConstraintName == UniqueConstraints.LanguageIdName)
            {
                return Errors.Word.NameAlreadyExists(word.Name);
            }
        }

        return new CreateWordDto(word.Id);
    }
}
