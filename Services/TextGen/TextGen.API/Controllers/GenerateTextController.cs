using MediatR;
using Microsoft.AspNetCore.Mvc;
using TextGen.Application.Commands.GenerateText;
using TextGen.Application.Commands.GenerateVocabularyList;
using TextGen.Application.Commands.PromptText;
using TextGen.Application.Queries.GetDailyTopics;

namespace TextGen.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenerateTextController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> GenerateText([FromBody] GenerateTextCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("promptTest")]
    public async Task<IActionResult> PromptTest([FromBody] PromptTestCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("wordList")]
    public async Task<IActionResult> GenerateWordList([FromBody] GenerateWordListCommand command)
    {
        var result = await Mediator.Send(command);
        return Ok(result);
    }
    [HttpGet("dailyTopic")]
    public async Task<IActionResult> GetDailyTopicsQuery()
    {
        var query = new GetDailyTopicsQuery();
        var result = await Mediator.Send(query);
        return Ok(result);
    }
}