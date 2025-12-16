using MediatR;
using TextGen.Application.Models;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Models.Llm;
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

        if (userWordList == null) throw new Exception("Bu listeye ait kelime bulunamadı.");

        var wordsFromUserWordList = userWordList.Words.Select(w => w.Text).Distinct().ToList();

        var validationPrompt = _promptBuilder.BuildValidationPrompt(request.Topic, wordsFromUserWordList);

        var validationResult = await _llmClient.GenerateContentAsync<VocabularyValidationResponse>(validationPrompt, cancellationToken);

        if (!validationResult.IsValid)
        {
            // NOT: İstersen burada özel bir Exception fırlatıp Middleware'de yakalayabilirsin
            // Örnek: throw new BusinessRuleException(validationResult.Reason);
            throw new InvalidOperationException($"Metin oluşturulamadı: {validationResult.Reason}");
        }

        var generationPrompt = _promptBuilder.BuildGenerationPrompt(
            request.Level,
            request.Topic,
            request.MinWordCount,
            request.MaxWordCount,
            validationResult.ApprovedWords
        );

        var textResult = await _llmClient.GenerateContentAsync<LlmTextResponseModel>(generationPrompt, cancellationToken);

        var generatedText = new GeneratedText
        {
            Id = Guid.NewGuid(),
            UserId = userIdFromAuth,
            Title = textResult.Title,
            Content = textResult.Content,
            WordCount = textResult.WordCount,
            CreatedAt = DateTime.UtcNow
        };

        var textRequest = new GeneratedTextRequest
        {
            Id = Guid.NewGuid(),
            GeneratedTextId = generatedText.Id,
            Topic = request.Topic,
            Level = request.Level
        };

        // ToDo (fix): kaydedilecek kelime LLM'den alınacak
        textRequest.Keywords = validationResult.ApprovedWords.Select(word => new GeneratedTextKeyword
        {
            Id = Guid.NewGuid(),
            GeneratedTextRequestId = textRequest.Id,
            Keyword = word
        }).ToList();

        generatedText.Request = textRequest;

        _dbContext.GeneratedTexts.Add(generatedText);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new GenerateTextResponseModel
        {
            Title = textResult.Title,
            Content = textResult.Content,
            WordCount = textResult.WordCount,
            Level = request.Level,
            Topic = request.Topic
        };

    }
}