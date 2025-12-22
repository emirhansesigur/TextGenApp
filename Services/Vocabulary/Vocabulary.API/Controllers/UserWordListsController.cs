using Microsoft.AspNetCore.Mvc;
using Vocabulary.Application.Commands.UserWordLists;
using Vocabulary.Application.Queries.UserWordLists;

namespace Vocabulary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserWordListsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserWordList([FromBody] CreateUserWordListCommand command)
    {
        var result = await Mediator.Send(command);
        return CreatedAtAction(nameof(GetUserWordList), new { id = result.Id }, result);
    }

    // Dıştan istek gelmesini engelle
    [HttpPost("Generated")]
    public async Task<IActionResult> CreateGeneratedWordList([FromBody] CreateGeneratedWordListCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserWordList(Guid id)
    {
        var query = new GetUserWordListQuery { Id = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("ByUser")]
    public async Task<IActionResult> GetUserWordListsByUser()
    {
        var query = new GetUserWordListsByUserQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserWordList(Guid id, [FromBody] UpdateUserWordListCommand command)
    {
        command.Id = id;

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserWordList(Guid id)
    {
        var command = new DeleteUserWordListCommand { Id = id };
        var result = await Mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
