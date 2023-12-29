using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

public class DeleteWordCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<DeleteWordCommand>
{
    public async Task Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        var (languageId, name) = request.DeleteWordDto;

        var word = await databaseContext.Words
            .FirstOrDefaultAsync(x => x.LanguageId == languageId && x.Name == name, cancellationToken);

        if (word is null) // TODO Do not throw exception. Just log information and return Maybe?
            throw new InvalidOperationException("Error deleting word. Word can't be deleted because it doesn't exist");

        databaseContext.Words.Remove(word);
        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
