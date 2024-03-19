using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Dictionary.Api.UseCases.Words.Commands.DeleteWord;

internal class DeleteWordCommandHandler(IDatabaseContext databaseContext)
    : IRequestHandler<DeleteWordCommand>
{
    public async Task Handle(DeleteWordCommand request, CancellationToken cancellationToken)
    {
        await databaseContext.Words.Where(x => x.Id == request.Id)
            .ExecuteDeleteAsync(cancellationToken: cancellationToken);
    }
}
