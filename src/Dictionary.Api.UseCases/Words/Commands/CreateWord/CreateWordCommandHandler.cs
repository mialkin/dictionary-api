using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.CreateWord;

internal class CreateWordCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<CreateWordCommand>
{
    public async Task Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        databaseContext.Words.Add(new Word(
            request.LanguageId,
            request.GenderId,
            request.Name,
            request.Translation,
            request.Transcription
        ));

        // TODO Use try/catch to check if uniqueness constraint is not respected. Write integration test that breaks
        // constraint and the appropriate error message is returned
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
