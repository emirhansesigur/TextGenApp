using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands.UserWords;

public class CreateUserWordCommand : UserWordRequestModel, IRequest<UserWord>
{
}

public class CreateUserWordCommandHandler(VocabularyDbContext _dbContext) : IRequestHandler<CreateUserWordCommand, UserWord>
{
    public async Task<UserWord> Handle(CreateUserWordCommand request, CancellationToken cancellationToken)
    {
        var userWord = new UserWord
        {
            Id = Guid.NewGuid(),
            UserWordListId = request.UserWordListId, // Validation needed
            Text = request.Text,
            Meaning = request.Meaning
        };

        _dbContext.UserWords.Add(userWord);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return userWord;
    }
}
