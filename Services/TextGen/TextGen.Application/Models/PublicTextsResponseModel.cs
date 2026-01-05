namespace TextGen.Application.Models;

public class PublicTextsResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string TextLevel { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public int WordCount { get; set; }
    public int EstimatedReadingTimeMinutes { get; set; }
}
