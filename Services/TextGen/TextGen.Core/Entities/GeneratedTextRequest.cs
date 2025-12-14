namespace TextGen.Core.Entities;

public class GeneratedTextRequest : BaseEntity
{
    public Guid GeneratedTextId { get; set; }
    public string Level { get; set; } = null!; 
    public string Topic { get; set; } = null!; 
    public GeneratedText GeneratedText { get; set; } = null!;
    public List<GeneratedTextKeyword> Keywords { get; set; } = new();
}