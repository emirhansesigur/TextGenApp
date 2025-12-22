using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Application.Models.DataTransfer;

public class WordListGenerationDto
{
    public string ListName { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public List<WordItemDto> Words { get; set; } = new();
}

public class WordItemDto
{
    public string Word { get; set; } = string.Empty;
    public string Meaning { get; set; } = string.Empty;
}