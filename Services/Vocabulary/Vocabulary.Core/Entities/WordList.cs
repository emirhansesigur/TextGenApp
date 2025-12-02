using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Core.Entities;


public class WordList : BaseEntity
{
    public Guid UserId { get; set; }

    public string Name { get; set; } = default!;

    public string? Level { get; set; } 

    public List<Word> Words { get; set; } = new();
}