namespace TextGen.Application.Models;

public class TextGenerationRequestModel
{
    public Guid UserId { get; set; }
    public Guid UserWordListId { get; set; }
    public string Topic { get; set; } = null!;
    public string Level { get; set; } = null!;
    public int MinWordCount { get; set; }
    public int MaxWordCount { get; set; }
}