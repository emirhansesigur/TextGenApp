using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Models;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Queries;

public class GetWordListQuery : IRequest<WordListResponseModel>
{
    public Guid Id { get; set; }
}

public class GetWordListQueryHandler : IRequestHandler<GetWordListQuery, WordListResponseModel>
{
    private readonly VocabularyDbContext _dbContext;

    public GetWordListQueryHandler(VocabularyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WordListResponseModel> Handle(GetWordListQuery request, CancellationToken cancellationToken)
    {
        var wordList = await _dbContext.WordLists
            .Include(wl => wl.Words)
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (wordList == null)
        {
            throw new KeyNotFoundException($"WordList with Id {request.Id} not found.");
        }

        return new WordListResponseModel
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
        };
    }
}