using MediatR;
using TextGen.Application.Models;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Services;
using TextGen.Core.Entities;

namespace TextGen.Application.Commands.GenerateText;

public class GenerateTextCommand : TextGenerationRequestModel, IRequest<GenerateTextResponseModel>
{
}

public class GenerateTextCommandHandler(ITextGenDbContext _dbContext, IVocabularyService _vocabularyService,
    ILlmClient _llmClient, PromptBuilder _promptBuilder) : IRequestHandler<GenerateTextCommand, GenerateTextResponseModel>
{
    public async Task<GenerateTextResponseModel> Handle(GenerateTextCommand request, CancellationToken cancellationToken)
    {
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");

        UserWordListDto userWordList = await _vocabularyService.GetUserWordListAsync(request.UserWordListId, cancellationToken);

        if (userWordList == null)
            throw new Exception("Bu listeye ait kelime bulunamadı.");

        var wordsFromUserWordList = userWordList.Words
            .Select(w => w.Text)
            .Distinct()
            .ToList();

        var prompt = _promptBuilder
                    .WithLevel(request.Level)
                    .WithTopic(request.Topic)
                    .WithWordRange(request.MinWordCount, request.MaxWordCount)
                    .WithWords(wordsFromUserWordList)
                    .Build();

        var llmResponse = await _llmClient.GenerateTextAsync(prompt, cancellationToken);

        var generatedText = new GeneratedText
        {
            Id = Guid.NewGuid(),
            UserId = userIdFromAuth,
            Title = llmResponse.Title,
            Content = llmResponse.Content,
            WordCount = llmResponse.WordCount,
            CreatedAt = DateTime.UtcNow
        };

        var textRequest = new GeneratedTextRequest
        {
            Id = Guid.NewGuid(),
            GeneratedTextId = generatedText.Id,
            Topic = request.Topic,
            Level = request.Level
        };

        textRequest.Keywords = wordsFromUserWordList.Select(word => new GeneratedTextKeyword
        {
            Id = Guid.NewGuid(),
            GeneratedTextRequestId = textRequest.Id, // FK, Request'i gösteriyor
            Keyword = word
        }).ToList();

        generatedText.Request = textRequest;

        _dbContext.GeneratedTexts.Add(generatedText);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GenerateTextResponseModel
        {
            Title = llmResponse.Title,
            Content = llmResponse.Content,
            WordCount = llmResponse.WordCount,
            Level = request.Level,
            Topic = request.Topic
        };
    }
}