using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.UseCases.Words.Commands.CreateWord;

public class CreateWordCommandHandler(IDatabaseContext databaseContext) : IRequestHandler<CreateWordCommand>
{
    public async Task Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateWordDto;

        databaseContext.Words.Add(new Word(
            dto.LanguageId,
            dto.Name,
            dto.Transcription,
            dto.Translation
        ));

        await databaseContext.SaveChangesAsync(cancellationToken);
    }
}
