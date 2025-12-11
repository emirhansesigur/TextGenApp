namespace TextGen.Application.Models;

public class GenerateTextResponseModel
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public string Level { get; set; } = null!;
    public int WordCount { get; set; }
}