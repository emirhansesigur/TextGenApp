using System.Reflection.Emit;
using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;

public class PromptBuilder
{
    // Template'leri hafızada tut
    private readonly string _generationTemplate;
    private readonly string _validationTemplate;
    private readonly string _topicSuggestionTemplate;
    private readonly string _wordListGenerationTemplate;

    public PromptBuilder()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;

        _generationTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "TextGenerationPrompt.txt"));
        _validationTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "VocabularyValidationPrompt.txt"));
        _topicSuggestionTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "TopicSuggestionPrompt.txt"));
        _wordListGenerationTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "WordListGenerationPrompt.txt"));
    }

    public string BuildGenerationPrompt(string level, string topic, int min, int max, List<string> approvedWords)
    {
        var wordsString = string.Join(", ", approvedWords);

        return _generationTemplate
            .Replace("{{Level}}", level)
            .Replace("{{Topic}}", topic)
            .Replace("{{MinWordCount}}", min.ToString())
            .Replace("{{MaxWordCount}}", max.ToString())
            .Replace("{{Words}}", wordsString);
    }

    public string BuildValidationPrompt(string topic, List<string> candidateWords)
    {
        var wordsString = string.Join(", ", candidateWords);

        var x = _validationTemplate
            .Replace("{{Topic}}", topic)
            .Replace("{{Words}}", wordsString);

        return x;
    }

    public string BuildTopicSuggestionPrompt()
    {
        return _topicSuggestionTemplate;
    }

    public string BuildWordListGenerationPrompt(string userRequest)
    {
        return _wordListGenerationTemplate
            .Replace("{{UserRequest}}", userRequest);
    }
}
