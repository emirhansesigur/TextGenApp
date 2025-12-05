using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands.UserWords;

public class UpdateUserWordCommand : UserWordRequestModel, IRequest<UserWord>
{
    public Guid Id { get; set; }
}

public class UpdateUserWordCommandHandler (VocabularyDbContext _dbContext) : IRequestHandler<UpdateUserWordCommand, UserWord>
{
    public async Task<UserWord> Handle(UpdateUserWordCommand request, CancellationToken cancellationToken)
    {
        var userWord = await _dbContext.UserWords
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        if (userWord == null)
        {
            throw new KeyNotFoundException($"UserWord with Id {request.Id} not found.");
        }

        userWord.Text = request.Text;
        userWord.Meaning = request.Meaning;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return userWord;
    }
}
