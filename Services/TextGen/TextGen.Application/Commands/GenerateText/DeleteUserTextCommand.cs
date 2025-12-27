using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Services;

namespace TextGen.Application.Commands.GenerateText;

public class DeleteUserTextCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
public class DeleteUserTextCommandHandler(ITextGenDbContext _dbContext) : IRequestHandler<DeleteUserTextCommand, bool>
{
    public async Task<bool> Handle(DeleteUserTextCommand request, CancellationToken cancellationToken)
    {
        var generatedText = await _dbContext.GeneratedTexts
                   .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (generatedText == null)
            return false;

        _dbContext.GeneratedTexts.Remove(generatedText);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
