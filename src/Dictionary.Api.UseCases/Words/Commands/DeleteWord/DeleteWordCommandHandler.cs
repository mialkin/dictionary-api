using CSharpFunctionalExtensions;
using Dictionary.Api.Domain;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

public class DeleteWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<DeleteWordCommand, UnitResult<Error>>
{
    public async Task<UnitResult<Error>> Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        var word = await databaseContext.Words.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (word is null)
            return UnitResult.Failure(error: Errors.General.NotFound(request.Id));

        databaseContext.Words.Remove(word);
        await databaseContext.SaveChangesAsync(cancellationToken);

        return UnitResult.Success<Error>();
    }
}
