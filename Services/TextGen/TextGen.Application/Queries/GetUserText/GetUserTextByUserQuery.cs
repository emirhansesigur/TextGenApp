using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetGeneratedText;

public class GetUserTextByUserQuery : IRequest<List<UserTextsResponseModel>>
{
}
public class GetUserTextByUserQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetUserTextByUserQuery, List<UserTextsResponseModel>>
{
    public async Task<List<UserTextsResponseModel>> Handle(GetUserTextByUserQuery request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");
        
        var generatedTexts = await _dbContext.GeneratedTexts
            .Where(gt => gt.UserId == userIdFromAuth)
            .Select(gt => new UserTextsResponseModel
            {
                Id = gt.Id,
                Title = gt.Title,
                Category = gt.Category,
                Summary = gt.Summary,
                WordCount = gt.WordCount,
                EstimatedReadingTimeMinutes = gt.EstimatedReadingTimeMinutes
            })
            .ToListAsync();

        return generatedTexts;
    }
}