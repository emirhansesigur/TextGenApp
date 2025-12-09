namespace TextGen.Application.Models;

public class LlmTextResponse
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
}