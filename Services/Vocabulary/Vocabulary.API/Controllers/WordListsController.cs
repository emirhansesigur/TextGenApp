using Microsoft.AspNetCore.Mvc;
using Vocabulary.Application.Services;

namespace Vocabulary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordListsController : ControllerBase
{
    private readonly WordListService _service;

    public WordListsController(WordListService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateWordListRequest request)
    {
        var result = await _service.CreateAsync(request.UserId, request.Name, request.Level);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateWordListRequest request)
    {
        var updatedWordList = await _service.UpdateAsync(id, request.Name, request.Level);

        return Ok(updatedWordList);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(Guid userId)
    {
        var lists = await _service.GetByUserAsync(userId);
        return Ok(lists);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var list = await _service.GetWithWordsAsync(id);
        return Ok(list);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { Message = "Word list deleted successfully" });
    }

}

public record CreateWordListRequest(Guid UserId, string Name, string? Level);
public record UpdateWordListRequest(string Name, string? Level);