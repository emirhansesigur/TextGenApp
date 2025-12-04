using System;

namespace Vocabulary.Application.Models;

public class WordResponseModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
}