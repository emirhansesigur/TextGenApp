using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Interfaces;

namespace Vocabulary.Application.Commands.UserWordLists;

public class DeleteUserWordListCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteUserWordListCommandHandler(IVocabularyDbContext _dbContext) : IRequestHandler<DeleteUserWordListCommand, bool>
{
    public async Task<bool> Handle(DeleteUserWordListCommand request, CancellationToken cancellationToken)
    {
        var userWordList = await _dbContext.UserWordLists
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (userWordList == null)
        {
            return false;
        }

        _dbContext.UserWordLists.Remove(userWordList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
