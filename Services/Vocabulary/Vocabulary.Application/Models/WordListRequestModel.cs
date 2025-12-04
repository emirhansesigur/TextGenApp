using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Application.Models;

public class WordListRequestModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public List<WordRequestModel> Words { get; set; } = new();
}
