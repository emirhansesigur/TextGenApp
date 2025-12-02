using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Core.Entities;

public class Word : BaseEntity
{
    public Guid WordListId { get; set; }
    public string Text { get; set; } = default!;
}