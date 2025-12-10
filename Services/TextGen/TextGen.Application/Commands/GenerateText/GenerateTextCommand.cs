using MediatR;
using TextGen.Application.Interfaces;
using TextGen.Application.Models;
using TextGen.Application.Services;
using TextGen.Core.Entities;

namespace TextGen.Application.Commands.GenerateText;

public class GenerateTextCommand : TextGenerationRequestModel, IRequest<GenerateTextResult>
{
}

public class GenerateTextCommandHandler(ITextGenDbContext _dbContext, IVocabularyService _vocabularyService,
    ILlmClient _llmClient, PromptBuilder _promptBuilder) : IRequestHandler<GenerateTextCommand, GenerateTextResult>
{
    public async Task<GenerateTextResult> Handle(GenerateTextCommand request, CancellationToken cancellationToken)
    {
        // 1. Kullanıcının kelime listesini getir
        var userIdFromAuth = Guid.Parse("00000000-0000-0000-0000-000000000001");

        //var wordListDtos = await _vocabularyService.GetUserWordListsAsync();

        //if (wordListDtos == null || !wordListDtos.Any())
        //    throw new Exception("Bu listeye ait kelime bulunamadı.");

        //var allWords = wordListDtos.SelectMany(dto => dto.Words).Distinct().ToList();


        // 2. Prompt oluşturma
        
        var prompt = _promptBuilder
                    .WithLevel(request.Level)
                    .WithTopic(request.Topic)
                    .WithWordRange(request.MinWordCount, request.MaxWordCount)
                    //.WithWords(wordListDtos)
                    .Build();

        // 3. LLM API çağrısı
        var llmResponse = await _llmClient.GenerateTextAsync(prompt, cancellationToken);

        // 4. GeneratedText kaydı oluştur
        var generatedText = new GeneratedText
        {
            Id = Guid.NewGuid(),
            UserId = userIdFromAuth,
            Title = llmResponse.Title,
            Content = llmResponse.Content,
            Topic = request.Topic,
            Level = request.Level,
            WordCount = llmResponse.WordCount,
            CreatedAt = DateTime.UtcNow
        };

        _dbContext.GeneratedTexts.Add(generatedText);

        // 5. Kelime eşleşmeleri
        //foreach (var keyword in allWords)
        //{
        //    _dbContext.GeneratedTextKeywords.Add(new GeneratedTextKeyword
        //    {
        //        Id = Guid.NewGuid(),
        //        GeneratedTextId = generatedText.Id,
        //        Keyword = keyword
        //    });
        //}

        await _dbContext.SaveChangesAsync(cancellationToken);

        // 6. Kullanıcıya dönüş
        return new GenerateTextResult
        {
            Title = llmResponse.Title,
            Content = llmResponse.Content,
            WordCount = llmResponse.WordCount,
            Level = request.Level,
            Topic = request.Topic
        };
    }
}