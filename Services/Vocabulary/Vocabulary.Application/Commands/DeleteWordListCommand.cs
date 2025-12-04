using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands;

public class DeleteWordListCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteWordListCommandHandler : IRequestHandler<DeleteWordListCommand, bool>
{
    private readonly VocabularyDbContext _dbContext;

    public DeleteWordListCommandHandler(VocabularyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> Handle(DeleteWordListCommand request, CancellationToken cancellationToken)
    {
        var wordList = await _dbContext.WordLists
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (wordList == null)
        {
            return false;
        }

        _dbContext.WordLists.Remove(wordList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
