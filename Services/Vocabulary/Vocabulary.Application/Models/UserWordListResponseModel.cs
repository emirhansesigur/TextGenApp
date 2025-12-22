using System;
using System.Collections.Generic;
using System.Text;

namespace Vocabulary.Application.Models;

public class UserWordListResponseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public List<UserWordResponseModel> UserWords { get; set; } = new();
}
