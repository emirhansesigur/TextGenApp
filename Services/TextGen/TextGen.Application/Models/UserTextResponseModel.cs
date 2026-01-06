namespace TextGen.Application.Models;

public class UserTextResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
    public List<UserTextQuizItemModel> Quiz { get; set; } = new();
    public string Category { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int EstimatedReadingTimeMinutes { get; set; }
}
public class UserTextQuizItemModel
{
    public Guid Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
}