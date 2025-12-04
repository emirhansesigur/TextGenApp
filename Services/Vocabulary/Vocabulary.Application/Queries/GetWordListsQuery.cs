using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Models;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Queries;

public class GetWordListsQuery : IRequest<List<WordListResponseModel>>
{
}

public class GetWordListsQueryHandler : IRequestHandler<GetWordListsQuery, List<WordListResponseModel>>
{
    private readonly VocabularyDbContext _dbContext;

    public GetWordListsQueryHandler(VocabularyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<WordListResponseModel>> Handle(GetWordListsQuery request, CancellationToken cancellationToken)
    {
        var wordLists = await _dbContext.WordLists
            .Include(wl => wl.Words)
            .ToListAsync(cancellationToken);

        return wordLists.Select(wordList => new WordListResponseModel
        {
            Id = wordList.Id,
            Name = wordList.Name,
            Level = wordList.Level,
            UserId = wordList.UserId,
            Words = wordList.Words.Select(word => new WordResponseModel
            {
                Id = word.Id,
                Text = word.Text
            }).ToList()
        }).ToList();
    }
}