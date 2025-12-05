using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Core.Entities;

public class UserWord : BaseEntity
{
    public Guid UserWordListId { get; set; }
    public string Text { get; set; } = default!;
    public string? Meaning { get; set; }
}