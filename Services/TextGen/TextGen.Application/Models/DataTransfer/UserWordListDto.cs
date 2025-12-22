namespace TextGen.Application.Models.DataTransfer;

public class UserWordListDto
{
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public List<UserWordDto> UserWords { get; set; } = new();
}

public class UserWordDto
{
    public string Text { get; set; } = string.Empty;
    public string Meaning { get; set; } = string.Empty; 
}