using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Core.Entities;

public class GeneratedTextKeyword : BaseEntity
{
    public Guid GeneratedTextId { get; set; }
    public string Keyword { get; set; } = null!;
    public GeneratedText GeneratedText { get; set; } = null!;
}
