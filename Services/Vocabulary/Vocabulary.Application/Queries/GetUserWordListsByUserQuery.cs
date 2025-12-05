using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vocabulary.Application.Models;
using Vocabulary.Infrastructure.Data;

namespace Vocabulary.Application.Queries;

public class GetUserWordListsByUserQuery : IRequest<List<UserWordListResponseModel>>
{
}

public class GetUserWordListsByUserQueryHandler : IRequestHandler<GetUserWordListsByUserQuery, List<UserWordListResponseModel>>
{
    private readonly VocabularyDbContext _dbContext;

    public GetUserWordListsByUserQueryHandler(VocabularyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserWordListResponseModel>> Handle(GetUserWordListsByUserQuery request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");
        var userWordLists = await _dbContext.UserWordLists
            .Include(wl => wl.UserWords)
            .Where(wl => wl.UserId == userIdFromAuth)
            .ToListAsync(cancellationToken);

        return userWordLists.Select(userWordList => new UserWordListResponseModel
        {
            Id = userWordList.Id,
            Name = userWordList.Name,
            Level = userWordList.Level,
            UserId = userWordList.UserId,
            Words = userWordList.UserWords.Select(word => new UserWordResponseModel
            {
                Id = word.Id,
                Text = word.Text
            }).ToList()
        }).ToList();
    }
}