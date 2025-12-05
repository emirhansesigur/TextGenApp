using System;

namespace Vocabulary.Application.Models;

public class UserWordRequestModel
{
    public string Text { get; set; } = string.Empty;
    public string? Meaning { get; set; }
}