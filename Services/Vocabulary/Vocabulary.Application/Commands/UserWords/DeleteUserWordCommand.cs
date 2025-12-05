using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vocabulary.Core.Entities;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands.UserWords;

public class DeleteUserWordCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}

public class DeleteUserWordCommandHandler(VocabularyDbContext _dbContext) : IRequestHandler<DeleteUserWordCommand, bool>
{
    public async Task<bool> Handle(DeleteUserWordCommand request, CancellationToken cancellationToken)
    {
        var userWord = await _dbContext.UserWords
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        if (userWord == null)
        {
            return false;
        }

        _dbContext.UserWords.Remove(userWord);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
