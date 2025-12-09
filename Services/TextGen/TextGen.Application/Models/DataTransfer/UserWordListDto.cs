using System;
using System.Collections.Generic;
using System.Text;

namespace TextGen.Application.Models.DataTransfer;

public class UserWordListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> Words { get; set; }
}