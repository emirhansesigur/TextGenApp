using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Application.Models.Llm;

public class WordListGenerationResponseModel
{
    public string ListName { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public List<WordItemModel> UserWords { get; set; } = new();
}

public class WordItemModel
{
    public string Text { get; set; } = string.Empty;
    public string Meaning { get; set; } = string.Empty;
}