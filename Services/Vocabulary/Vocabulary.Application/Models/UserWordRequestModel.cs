using System;

namespace Vocabulary.Application.Models;

public class UserWordRequestModel
{
    public Guid UserWordListId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? Meaning { get; set; }
}