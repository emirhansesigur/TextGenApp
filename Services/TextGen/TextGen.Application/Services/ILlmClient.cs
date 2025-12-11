using TextGen.Application.Models;
namespace TextGen.Application.Services;
public interface ILlmClient
{
    Task<LlmTextResponseModel> GenerateTextAsync(string prompt, CancellationToken cancellationToken);
}