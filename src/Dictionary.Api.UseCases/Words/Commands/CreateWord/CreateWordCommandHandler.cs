using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

internal class CreateWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<CreateWordCommand, Result<CreateWordDto, Error>>
{
    public async Task<Result<CreateWordDto, Error>> Handle(
        CreateWordCommand request,
        CancellationToken cancellationToken)
    {
        var unitResult = Word.CanCreate(request.Name, request.Transcription, request.Translation);
        if (unitResult.IsFailure)
        {
            return unitResult.Error;
        }

        var word = Word.Create(request.LanguageId, request.Name, request.Transcription, request.Translation);

        databaseContext.Words.Add(word);

        // TODO Use try/catch to check if uniqueness constraint is not respected. Write integration test that breaks
        // constraint and the appropriate error message is returned
        try
        {
            await databaseContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            var type = exception.GetType();

            return Errors.Word.SomethingWentWrong();
        }

        return new CreateWordDto(word.Id);
    }
}
