namespace TextGen.Core.Entities;

public class GeneratedText : BaseEntity
{
    public Guid UserId { get; set; }

    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }

    public GeneratedTextRequest Request { get; set; } = null!;
}