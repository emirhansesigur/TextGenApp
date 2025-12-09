using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Interfaces;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;

namespace Vocabulary.Application.Commands.UserWordLists;

public class UpdateUserWordListCommand : UserWordListRequestModel, IRequest<UserWordList>
{
    public Guid Id { get; set; }
}

public class UpdateUserWordListCommandHandler(IVocabularyDbContext _dbContext) : IRequestHandler<UpdateUserWordListCommand, UserWordList>
{
    public async Task<UserWordList> Handle(UpdateUserWordListCommand request, CancellationToken cancellationToken)
    {
        var userWordList = await _dbContext.UserWordLists
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (userWordList == null)
        {
            throw new KeyNotFoundException($"userWordList with Id {request.Id} not found.");
        }

        userWordList.Name = request.Name;
        userWordList.Level = request.Level;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return userWordList;
    }
}