using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Application.Models;

public class UserWordListRequestModel
{
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
}
