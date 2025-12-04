using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands;

public class UpdateWordListCommand : WordListRequestModel, IRequest<WordList>
{
    public Guid Id { get; set; }
}

public class UpdateWordListCommandHandler : IRequestHandler<UpdateWordListCommand, WordList>
{
    private readonly VocabularyDbContext _dbContext;

    public UpdateWordListCommandHandler(VocabularyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WordList> Handle(UpdateWordListCommand request, CancellationToken cancellationToken)
    {
        var wordList = await _dbContext.WordLists
            .Include(wl => wl.Words)
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (wordList == null)
        {
            throw new KeyNotFoundException($"WordList with Id {request.Id} not found.");
        }

        wordList.Name = request.Name;
        wordList.Level = request.Level;
        wordList.UserId = request.UserId;

        // Update Words
        wordList.Words.Clear();
        wordList.Words.AddRange(request.Words.Select(word => new Word
        {
            Id = Guid.NewGuid(),
            Text = word.Text,
            CreatedAt = DateTime.UtcNow
        }));

        await _dbContext.SaveChangesAsync(cancellationToken);

        return wordList;
    }
}