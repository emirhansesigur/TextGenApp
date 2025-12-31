using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetDailyTopics;

public class GetDailyTopicsQuery : IRequest<List<DailyTopicResponseModel>>
{
}
public class GetDailyTopicsQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetDailyTopicsQuery, List<DailyTopicResponseModel>>
{
    public async Task<List<DailyTopicResponseModel>> Handle(GetDailyTopicsQuery request, CancellationToken cancellationToken)
    {
        var topics = await _dbContext.SuggestedTopics.OrderByDescending(x=>x.CreatedAt).ToListAsync(cancellationToken);

        if (!topics.Any())
        {
            return [];
        }

        return topics.Select(dt => new DailyTopicResponseModel
        {
            Id = dt.Id,
            Title = dt.Title,
            Category = dt.Category,
            Content = dt.Content,
            CreatedAt = dt.CreatedAt
        }).ToList();
    }
}