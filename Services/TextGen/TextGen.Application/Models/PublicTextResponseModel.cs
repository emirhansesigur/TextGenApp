namespace TextGen.Application.Models;

public class PublicTextResponseModel
{
    public string Title { get; set; } = null!;
    public string Category { get; set; } = null!;
    public string TextLevel { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
    public List<PublicTextQuizItemModel> Quiz { get; set; } = new();
}

public class PublicTextQuizItemModel
{
    public string Question { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new();
}