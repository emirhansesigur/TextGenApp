using TextGen.Application.Models.DataTransfer;

namespace TextGen.Application.Services;

public class PromptBuilder
{
    private List<string> _words = new();
    private string? _level;
    private string? _topic;
    private int _minWordCount;
    private int _maxWordCount;

    public PromptBuilder WithWords(List<string> words)
    {
        _words = words;
        return this;
    }

    public PromptBuilder WithLevel(string level)
    {
        _level = level;
        return this;
    }

    public PromptBuilder WithTopic(string topic)
    {
        _topic = topic;
        return this;
    }

    public PromptBuilder WithWordRange(int min, int max)
    {
        _minWordCount = min;
        _maxWordCount = max;
        return this;
    }

    public string Build()
    {
        // Prod'da Cache'lemek performans artırır)
        // Dosya yolunu AppDomain.CurrentDomain.BaseDirectory ile dinamik bul
        var templatePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates", "TextGenerationPrompt.txt");
        var template = File.ReadAllText(templatePath);

        var requiredWords = _words.Any()
            ? string.Join(", ", _words)
            : "No specific vocabulary required.";

        // 2. Yer tutucuları (Placeholders) değiştir
        
        var prompt = template
            .Replace("{{Level}}", _level)
            .Replace("{{Topic}}", _topic)
            .Replace("{{MinWordCount}}", _minWordCount.ToString())
            .Replace("{{MaxWordCount}}", _maxWordCount.ToString())
            .Replace("{{Words}}", requiredWords);

        return prompt;


    }


}
