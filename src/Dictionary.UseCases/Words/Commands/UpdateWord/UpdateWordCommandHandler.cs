using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.UseCases.Words.Commands.UpdateWord;

public class UpdateWordCommandHandler : IRequestHandler<UpdateWordCommand>
{
    private readonly IDatabaseContext _databaseContext;

    public UpdateWordCommandHandler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

    public async Task Handle(UpdateWordCommand request, CancellationToken cancellationToken)
    {
        var dto = request.UpdateWordDto;
        var word = await _databaseContext.Words.FindAsync(dto.GetPrimaryKey(), cancellationToken);

        if (word is null) // TODO Do not throw exception. Just log information and return Maybe?
            throw new InvalidOperationException("Error updating word. Word can't be updated because it doesn't exist");

        word.Update(dto.Transcription, dto.Translation);

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
