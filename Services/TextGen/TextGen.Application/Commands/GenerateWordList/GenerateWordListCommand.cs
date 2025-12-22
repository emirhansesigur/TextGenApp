using MediatR;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Models.Llm;
using TextGen.Application.Services;

namespace TextGen.Application.Commands.GenerateVocabularyList;

public class GenerateWordListCommand : IRequest<WordListGenerationResponseModel>
{
    public string UserPrompt { get; set; } = string.Empty;
}

public class GenerateVocabularyListCommandHandler(ILlmClient _llmClient,
    PromptBuilder _promptBuilder, IVocabularyService _vocabularyService) : IRequestHandler<GenerateWordListCommand, WordListGenerationResponseModel>
{
    public async Task<WordListGenerationResponseModel> Handle(GenerateWordListCommand request, CancellationToken cancellationToken)
    {
        var prompt = _promptBuilder.BuildWordListGenerationPrompt(request.UserPrompt);

        var result = await _llmClient.GenerateContentAsync<WordListGenerationResponseModel>(prompt, cancellationToken);

        if (result == null) throw new Exception("Kelime listesi oluşturulamadı.");

        var userWordListDto = new UserWordListDto
        {
            Name = result.ListName,
            Level = result.Level,
            UserWords = result.UserWords.Select(w => new UserWordDto
            {
                Text = w.Text,
                Meaning = w.Meaning
            }).ToList()
        };
        var isSuccess = await _vocabularyService.SaveGeneratedWordListAsync(userWordListDto, cancellationToken);

        if (!isSuccess)
        {
            throw new Exception("Kelime listesi oluşturuldu ancak veritabanına kaydedilemedi.");
        }

        return result;
    }
}