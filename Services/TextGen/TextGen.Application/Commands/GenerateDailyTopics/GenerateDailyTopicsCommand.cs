using MediatR;
using TextGen.Application.Models;
using TextGen.Application.Services;
using TextGen.Core.Entities;

namespace TextGen.Application.Commands.GenerateDailyTopics;

public class GenerateDailyTopicsCommand : IRequest<bool>
{
}

public class GenerateDailyTopicsCommandHandler(ITextGenDbContext _dbContext, ILlmClient _llmClient, 
    PromptBuilder _promptBuilder) : IRequestHandler<GenerateDailyTopicsCommand, bool>
{
    public async Task<bool> Handle(GenerateDailyTopicsCommand request, CancellationToken cancellationToken)
    {
        var prompt = _promptBuilder.BuildTopicSuggestionPrompt();

        // LLM'den listeyi alıyoruz
        var suggestedDtos = await _llmClient.GenerateContentAsync<List<TopicSuggestionDto>>(prompt, cancellationToken);

        if (suggestedDtos == null || !suggestedDtos.Any()) return false;

        var entities = suggestedDtos.Select(dto => new SuggestedTopic
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            Category = dto.Category,
            CreatedAt = DateTime.UtcNow
        }).ToList();

        await _dbContext.SuggestedTopics.AddRangeAsync(entities, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        Console.WriteLine($"--> Hangfire Başarılı: {entities.Count} yeni konu eklendi.");

        return true;
    }
}