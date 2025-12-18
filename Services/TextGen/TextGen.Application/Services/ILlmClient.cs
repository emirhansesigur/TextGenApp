namespace TextGen.Application.Services;
public interface ILlmClient
{
    Task<T> GenerateContentAsync<T>(string prompt, CancellationToken cancellationToken);
    Task<T> PromptTest<T>(string prompt, CancellationToken cancellationToken); 
}