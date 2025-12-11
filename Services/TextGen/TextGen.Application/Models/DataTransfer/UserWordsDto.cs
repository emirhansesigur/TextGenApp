namespace TextGen.Application.Models.DataTransfer;

public class UserWordDto
{
    public Guid Id { get; set; }
    public string Text { get; set; } = string.Empty;
    public string Meaning { get; set; }
}