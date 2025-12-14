using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Core.Entities;

public class GeneratedTextKeyword : BaseEntity
{
    public Guid GeneratedTextRequestId { get; set; }
    public string Keyword { get; set; } = null!;
    public GeneratedTextRequest GeneratedTextRequest { get; set; } = null!;
}