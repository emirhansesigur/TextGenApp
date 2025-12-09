namespace TextGen.Application.Services;

public class PromptBuilder
{
    private List<string> _words = new();
    private string? _level;
    private string? _topic;
    private int _minWordCount;
    private int _maxWordCount;

    public PromptBuilder WithWords(IEnumerable<string> words)
    {
        if (words != null)
            _words = words.ToList();

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
        var requiredWords = _words.Any()
            ? string.Join(", ", _words.Select(w => $"\"{w}\""))
            : string.Empty;

        return $@"
                You are an AI English text generator.

                Generate an English reading text using the requirements below:

                ### Requirements:
                - Level: {_level}
                - Topic: {_topic}
                - Word count: between {_minWordCount} and {_maxWordCount} words
                - Include the following target vocabulary words in the text naturally:
                  [{requiredWords}]

                ### Output format (must be STRICT JSON):
                {{
                  ""title"": """",
                  ""content"": """",
                  ""wordCount"": 0,
                  ""keywordsUsed"": []
                }}

                Only output valid JSON. No explanations.
                ";
    }
}
