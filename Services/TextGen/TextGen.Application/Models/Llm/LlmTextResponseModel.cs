namespace TextGen.Application.Models.Llm;

public class LlmTextResponseModel
{
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int EstimatedReadingTimeMinutes { get; set; }
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
    public List<string> KeywordsUsed { get; set; } = new List<string>();
    public List<LlmQuizItem> Quiz { get; set; }
}
public class LlmQuizItem
{
    public string Question { get; set; }
    public List<string> Options { get; set; }
    public int CorrectAnswer { get; set; } // 0, 1, 2, 3
}