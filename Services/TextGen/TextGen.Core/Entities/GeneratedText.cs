namespace TextGen.Core.Entities;

public class GeneratedText : BaseEntity
{
    public Guid UserId { get; set; }
    public string Category { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int EstimatedReadingTimeMinutes { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
    public List<TextQuizItem> Quiz { get; set; } = new();
    public GeneratedTextRequest Request { get; set; } = null!;
}
public class TextQuizItem
{
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
    public int CorrectAnswer { get; set; } // Index: 0, 1, 2, 3 
}