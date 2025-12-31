using Microsoft.AspNetCore.Mvc;
using TextGen.Application.Commands.GenerateText;
using TextGen.Application.Queries.GetDailyTopics;
using TextGen.Application.Queries.GetGeneratedText;

namespace TextGen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ContentController : ApiControllerBase
{
    [HttpGet("dailyTopic")]
    public async Task<IActionResult> GetDailyTopicsQuery()
    {
        var query = new GetDailyTopicsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("userText/{id:guid}")]
    public async Task<IActionResult> GetUserText(Guid id)
    {
        var query = new GetUserTextQuery { Id = id };
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("userTexts/byUser")]
    public async Task<IActionResult> GetUserTextsByUser()
    {
        var query = new GetUserTextByUserQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }
    
    [HttpDelete("userText/{id:guid}")]
    public async Task<IActionResult> DeleteUserText(Guid id)
    {
        var command = new DeleteUserTextCommand { Id = id };
        var result = await Mediator.Send(command);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}