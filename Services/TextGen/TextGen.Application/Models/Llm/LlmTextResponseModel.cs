namespace TextGen.Application.Models.Llm;

public class LlmTextResponseModel
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
    public List<string> KeywordsUsed { get; set; } = new List<string>();
}