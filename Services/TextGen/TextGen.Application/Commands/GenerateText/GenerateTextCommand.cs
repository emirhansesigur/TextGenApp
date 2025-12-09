using MediatR;
using TextGen.Application.Interfaces;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Application.Commands.GenerateText;

public class GenerateTextCommand : TextGenerationRequestModel, IRequest<GenerateTextResult>
{

}

public class GenerateTextCommandHandler(ITextGenDbContext textGenDb, ILlmClient llmClient, PromptBuilder promptBuilder) : IRequestHandler<GenerateTextCommand, GenerateTextResult>
{
    public async Task<GenerateTextResult> Handle(GenerateTextCommand request, CancellationToken cancellationToken)
    {
        //// 1. Kullanıcının kelime listesini getir
        //var keywords = await _vocabularyDb.UserWords
        //    .Where(w => w.UserWordListId == request.UserWordListId)
        //    .Select(w => w.Word)
        //    .ToListAsync(cancellationToken);

        //if (keywords.Count == 0)
        //    throw new Exception("Bu listeye ait kelime bulunamadı.");

        //// 2. Prompt oluştur
        //var prompt = _promptBuilder
        //    .SetTopic(request.Topic)
        //    .SetLevel(request.Level)
        //    .SetWordRange(request.MinWordCount, request.MaxWordCount)
        //    .SetKeywords(keywords)
        //    .Build();

        //// 3. LLM API çağrısı
        //var llmResponse = await _llmClient.GenerateTextAsync(prompt, cancellationToken);

        //// 4. GeneratedText kaydı oluştur
        //var generatedText = new GeneratedText
        //{
        //    Id = Guid.NewGuid(),
        //    UserId = request.UserId, // Kullanıcı ID'si servisten gelcek
        //    Title = llmResponse.Title,
        //    Content = llmResponse.Content,
        //    Topic = request.Topic,
        //    Level = request.Level,
        //    WordCount = llmResponse.WordCount,
        //    CreatedAt = DateTime.UtcNow
        //};

        //_textGenDb.GeneratedTexts.Add(generatedText);

        //// 5. Kelime eşleşmeleri
        //foreach (var keyword in keywords)
        //{
        //    _textGenDb.GeneratedTextKeywords.Add(new GeneratedTextKeyword
        //    {
        //        Id = Guid.NewGuid(),
        //        GeneratedTextId = generatedText.Id,
        //        Keyword = keyword
        //    });
        //}

        //await _textGenDb.SaveChangesAsync(cancellationToken);

        //// 6. Kullanıcıya dönüş
        //return new GenerateTextResult
        //{
        //    Title = llmResponse.Title,
        //    Content = llmResponse.Content,
        //    WordCount = llmResponse.WordCount,
        //    Level = request.Level,
        //    Topic = request.Topic
        //};
        throw new NotImplementedException();
    }
}