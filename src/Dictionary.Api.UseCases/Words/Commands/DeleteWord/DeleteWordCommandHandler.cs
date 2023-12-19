using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

public class DeleteWordCommandHandler : IRequestHandler<DeleteWordCommand>
{
    private readonly IDatabaseContext _databaseContext;

    public DeleteWordCommandHandler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

    public async Task Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        var (languageId, name) = request.DeleteWordDto;

        var word = await _databaseContext.Words
            .FirstOrDefaultAsync(x => x.LanguageId == languageId && x.Name == name, cancellationToken);

        if (word is null) // TODO Do not throw exception. Just log information and return Maybe?
            throw new InvalidOperationException("Error deleting word. Word can't be deleted because it doesn't exist");

        _databaseContext.Words.Remove(word);
        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
