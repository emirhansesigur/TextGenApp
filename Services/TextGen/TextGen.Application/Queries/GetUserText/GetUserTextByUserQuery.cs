using MediatR;
using Microsoft.EntityFrameworkCore;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Queries.GetGeneratedText;

public class GetUserTextByUserQuery : IRequest<List<UserTextResponseModel>>
{
}
public class GetUserTextByUserQueryHandler(ITextGenDbContext _dbContext) : IRequestHandler<GetUserTextByUserQuery, List<UserTextResponseModel>>
{
    public async Task<List<UserTextResponseModel>> Handle(GetUserTextByUserQuery request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");
        
        var generatedTexts = await _dbContext.GeneratedTexts
            .Where(gt => gt.UserId == userIdFromAuth)
            .Select(gt => new UserTextResponseModel
            {
                Id = gt.Id,
                Title = gt.Title,
                Content = gt.Content,
                WordCount = gt.WordCount
            })
            .ToListAsync();

        return generatedTexts;

    }
}