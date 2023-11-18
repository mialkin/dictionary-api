using Dictionary.Api.Domain.Entities;
using Dictionary.Api.Infrastructure.Interfaces.Database;
using MediatR;

namespace Dictionary.UseCases.Words.Commands.CreateWord;

public class CreateWordCommandHandler : IRequestHandler<CreateWordCommand>
{
    private readonly IDatabaseContext _databaseContext;

    public CreateWordCommandHandler(IDatabaseContext databaseContext) => _databaseContext = databaseContext;

    public async Task Handle(CreateWordCommand request, CancellationToken cancellationToken)
    {
        var dto = request.CreateWordDto;

        _databaseContext.Words.Add(new Word(
            dto.LanguageId,
            dto.Name,
            dto.Transcription,
            dto.Translation
        ));

        await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}
