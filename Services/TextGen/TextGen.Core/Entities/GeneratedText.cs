using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Core.Entities;

public class GeneratedText: BaseEntity
{
    public Guid UserId { get; set; }
    public Guid UserWordListId { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string Level { get; set; } = null!;
    public string Topic { get; set; } = null!;
    public int WordCount { get; set; }
    public List<GeneratedTextKeyword> Keywords { get; set; } = new();
}
