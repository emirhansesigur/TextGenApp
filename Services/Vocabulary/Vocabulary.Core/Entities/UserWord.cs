using Vocabulary.Core.Enum;

namespace Vocabulary.Core.Entities;

public class UserWord : BaseEntity
{
    public Guid UserWordListId { get; set; }
    public string Text { get; set; } = default!;
    public string? Meaning { get; set; }
    public LearningStatus Status { get; set; } = LearningStatus.ToLearn;
}