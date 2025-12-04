using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vocabulary.Application.Models;
using Vocabulary.Core.Entities;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Commands;

public class CreateWordListCommand : WordListRequestModel, IRequest<WordList>
{

}

public class CreateWordListCommandHandler(VocabularyDbContext _dbContext) : IRequestHandler<CreateWordListCommand, WordList>
{

    public async Task<WordList> Handle(CreateWordListCommand request, CancellationToken cancellationToken)
    {
        var wordList = new WordList
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Level = request.Level,
            UserId = request.UserId,
            Words = request.Words.Select(word => new Word
            {
                Id = Guid.NewGuid(),
                Text = word.Text
            }).ToList()
        };

        _dbContext.WordLists.Add(wordList);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return wordList;
    }
}