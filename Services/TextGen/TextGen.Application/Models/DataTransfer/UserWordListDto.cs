namespace TextGen.Application.Models.DataTransfer;

public class UserWordListDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public List<UserWordDto> Words { get; set; } = new();
}