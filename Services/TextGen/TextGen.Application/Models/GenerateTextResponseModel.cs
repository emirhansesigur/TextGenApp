namespace TextGen.Application.Models;

public class GenerateTextResponseModel
{
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int EstimatedReadingTimeMinutes { get; set; }
    public string Content { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string Level { get; set; } = null!;
    public int WordCount { get; set; }
    public List<string> Keywords { get; set; } = new List<string>();
    public List<QuizItemDto> Quiz { get; set; }
}
public class QuizItemDto
{
    public Guid Id { get; set; }
    public string Question { get; set; }
    public List<string> Options { get; set; }
}