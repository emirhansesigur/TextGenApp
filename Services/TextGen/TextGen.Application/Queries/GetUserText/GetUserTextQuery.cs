using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetGeneratedText;

public class GetUserTextQuery : IRequest<UserTextResponseModel>
{
    public Guid Id { get; set; }
}
public class GetUserTextQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetUserTextQuery, UserTextResponseModel>
{
    public async Task<UserTextResponseModel> Handle(GetUserTextQuery request, CancellationToken cancellationToken)
    {
        var generatedText = await _dbContext.GeneratedTexts.FirstOrDefaultAsync(gt => gt.Id == request.Id, cancellationToken);
        if (generatedText == null)
        {
            throw new KeyNotFoundException($"GeneratedText with Id {request.Id} not found.");
        }
        return new UserTextResponseModel
        {
            Id = generatedText.Id,
            Title = generatedText.Title,
            Content = generatedText.Content,
            WordCount = generatedText.WordCount,
            Category = generatedText.Category,
            Summary = generatedText.Summary,
            EstimatedReadingTimeMinutes = generatedText.EstimatedReadingTimeMinutes,
            Quiz = generatedText.Quiz.Select(q => new UserTextQuizItemModel
            {
                Id = q.Id,
                Question = q.Question,
                Options = q.Options
            }).ToList()
        };
    }
}