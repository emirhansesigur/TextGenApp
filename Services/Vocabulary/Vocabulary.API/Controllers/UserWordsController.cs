using Microsoft.AspNetCore.Mvc;
using Vocabulary.Application.Commands.UserWordLists;
using Vocabulary.Application.Commands.UserWords;

namespace Vocabulary.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserWordsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserWord([FromBody] CreateUserWordCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserWord(Guid id)
    {
        var command = new DeleteUserWordCommand { Id = id };
        var result = await Mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserWord(Guid id, [FromBody] UpdateUserWordCommand command)
    {
        command.Id = id;

        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPut("wordStatus/{id}")]
    public async Task<IActionResult> UpdateWordStatus(Guid id, [FromBody] UpdateWordStatusCommand command)
    {
        command.Id = id;

        var result = await Mediator.Send(command);
        return Ok(result);
    }

}