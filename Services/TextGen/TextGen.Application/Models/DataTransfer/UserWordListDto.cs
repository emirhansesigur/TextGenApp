using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Application.Models.DataTransfer;

public class UserWordListDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<string> Words { get; set; }
}