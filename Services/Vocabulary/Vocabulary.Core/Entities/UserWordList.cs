namespace Vocabulary.Core.Entities;

public class UserWordList : BaseEntity
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = default!;

    public string? Level { get; set; } 

    public List<UserWord> UserWords { get; set; } = new();
}