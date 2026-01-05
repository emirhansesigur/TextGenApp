namespace TextGen.Application.Models;

public class UserTextsResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public int WordCount { get; set; }
    public string Category { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int EstimatedReadingTimeMinutes { get; set; }
}