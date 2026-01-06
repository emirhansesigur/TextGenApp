using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetPublicText;

public class GetPublicTextQuery : IRequest<PublicTextResponseModel>
{
    public Guid Id { get; set; }
}

public class GetPublicTextQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetPublicTextQuery, PublicTextResponseModel>
{
    public async Task<PublicTextResponseModel> Handle(GetPublicTextQuery request, CancellationToken cancellationToken)
    {
        var publicText = await _dbContext.PublicTexts
                    .AsNoTracking()
                    .Where(x => x.Id == request.Id)
                    .Select(x => new PublicTextResponseModel
                    {
                        Title = x.Title,
                        Category = x.Category,
                        TextLevel = x.TextLevel,
                        Content = x.Content,
                        Summary = x.Summary,
                        WordCount = x.WordCount,
                        EstimatedReadingTimeMinutes = x.EstimatedReadingTimeMinutes,
                        Quiz = x.Quiz.Select(q => new PublicTextQuizItemModel
                        {
                            Id = q.Id,
                            Question = q.Question,
                            Options = q.Options
                        }).ToList()
                    })
                    .FirstOrDefaultAsync(cancellationToken);

        return publicText;
    }
}