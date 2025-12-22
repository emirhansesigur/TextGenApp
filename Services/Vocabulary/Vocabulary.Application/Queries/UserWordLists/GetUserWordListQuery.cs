using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Interfaces;
using Vocabulary.Application.Models;

namespace Vocabulary.Application.Queries.UserWordLists;

public class GetUserWordListQuery : IRequest<UserWordListResponseModel>
{
    public Guid Id { get; set; }
}

public class GetUserWordListQueryHandler(IVocabularyDbContext _dbContext) : IRequestHandler<GetUserWordListQuery, UserWordListResponseModel>
{
    public async Task<UserWordListResponseModel> Handle(GetUserWordListQuery request, CancellationToken cancellationToken)
    {
        var userWordList = await _dbContext.UserWordLists
            .Include(wl => wl.UserWords)
            .FirstOrDefaultAsync(wl => wl.Id == request.Id, cancellationToken);

        if (userWordList == null)
        {
            throw new KeyNotFoundException($"userWordList with Id {request.Id} not found.");
        }

        return new UserWordListResponseModel
        {
            Id = userWordList.Id,
            Name = userWordList.Name,
            Level = userWordList.Level,
            UserId = userWordList.UserId,
            UserWords = userWordList.UserWords.Select(word => new UserWordResponseModel
            {
                Id = word.Id,
                Text = word.Text,
                Meaning = word.Meaning,
                Status = word.Status
            }).ToList()
        };
    }
}