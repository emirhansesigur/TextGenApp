using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetPublicText;

public class GetPublicTextsQuery : IRequest<List<PublicTextsResponseModel>>
{
}

public class GetPublicTextsQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetPublicTextsQuery, List<PublicTextsResponseModel>>
{
    public async Task<List<PublicTextsResponseModel>> Handle(GetPublicTextsQuery request, CancellationToken cancellationToken)
    {
        var result = await _dbContext.PublicTexts
                    .AsNoTracking()
                    .Select(x => new PublicTextsResponseModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Category = x.Category,
                        TextLevel = x.TextLevel,
                        Summary = x.Summary,
                        WordCount = x.WordCount,
                        EstimatedReadingTimeMinutes = x.EstimatedReadingTimeMinutes
                    })
                    .ToListAsync(cancellationToken);

        return result;
    }
}