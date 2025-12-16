using System.Reflection.Emit;
using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;

public class PromptBuilder
{
    // Template'leri hafızada tut
    private readonly string _generationTemplate;
    private readonly string _validationTemplate;

    public PromptBuilder()
    {
        var basePath = AppDomain.CurrentDomain.BaseDirectory;

        _generationTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "TextGenerationPrompt.txt"));
        _validationTemplate = File.ReadAllText(Path.Combine(basePath, "Templates", "VocabularyValidationPrompt.txt"));
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
}
