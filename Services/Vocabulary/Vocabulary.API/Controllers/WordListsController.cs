using Microsoft.AspNetCore.Mvc;
using Vocabulary.Application.Commands;
using Vocabulary.Application.Queries;

namespace Vocabulary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WordListsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateWordList([FromBody] CreateWordListCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetWordList), new { id = result.Id }, result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetWordList(Guid id)
    {
        var query = new GetWordListQuery { Id = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetWordLists()
    {
        var query = new GetWordListsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWordList(Guid id, [FromBody] UpdateWordListCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest("ID in the URL does not match ID in the request body.");
        }

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWordList(Guid id)
    {
        var command = new DeleteWordListCommand { Id = id };
        var result = await Mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
