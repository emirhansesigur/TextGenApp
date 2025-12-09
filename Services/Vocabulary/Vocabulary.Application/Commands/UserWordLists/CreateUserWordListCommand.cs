using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vocabulary.Application.Interfaces;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;

namespace Vocabulary.Application.Commands.UserWordLists;

public class CreateUserWordListCommand : UserWordListRequestModel, IRequest<UserWordList>
{

}
public class CreateWordListCommandHandler(IVocabularyDbContext _dbContext) : IRequestHandler<CreateUserWordListCommand, UserWordList>
{
    public async Task<UserWordList> Handle(CreateUserWordListCommand request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001"); 
        var userWordList = new UserWordList
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Level = request.Level,
            UserId = userIdFromAuth,
        };

        _dbContext.UserWordLists.Add(userWordList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userWordList;
    }
}
