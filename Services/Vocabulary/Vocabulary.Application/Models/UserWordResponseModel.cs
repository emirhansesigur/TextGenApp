using System;
using Vocabulary.Core.Enum;

namespace Vocabulary.Application.Models;

public class UserWordResponseModel
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Meaning { get; set; }
    public LearningStatus Status { get; set; }
}