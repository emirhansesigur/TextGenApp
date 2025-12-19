namespace TextGen.Core.Entities;

public class SuggestedTopic : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
}