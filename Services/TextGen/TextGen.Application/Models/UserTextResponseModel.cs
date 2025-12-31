namespace TextGen.Application.Models;

public class UserTextResponseModel
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public int WordCount { get; set; }
}
