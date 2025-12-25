using Microsoft.AspNetCore.Mvc;
using TextGen.Application.Commands.GenerateText;
using TextGen.Application.Commands.GenerateVocabularyList;
using TextGen.Application.Commands.PromptText;

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
}