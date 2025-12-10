using MediatR;
using Microsoft.AspNetCore.Mvc;
using TextGen.Application.Commands.GenerateText;

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
}