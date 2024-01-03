using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.Api.UseCases.Words.Commands.UpdateWord;

public class UpdateWordCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<UpdateWordCommand>
{
    public async Task Handle(UpdateWordCommand request, CancellationToken cancellationToken)
    {
        var word = await databaseContext.Words.FindAsync(request.Id, cancellationToken);

        if (word is null) // TODO Do not throw exception. Just log information and return Maybe?
            throw new InvalidOperationException("Error updating word. Word can't be updated because it doesn't exist");

        word.Update(request.Translation, request.Transcription);

        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
