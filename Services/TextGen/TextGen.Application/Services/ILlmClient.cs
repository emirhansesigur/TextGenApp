using TextGen.Application.Models;
namespace TextGen.Application.Services;
public interface ILlmClient
{
    Task<LlmTextResponse> GenerateTextAsync(string prompt, CancellationToken cancellationToken);
}