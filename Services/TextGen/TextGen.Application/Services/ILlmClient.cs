namespace TextGen.Application.Services;
public interface ILlmClient
{
    Task<T> GenerateContentAsync<T>(string prompt, CancellationToken cancellationToken);
}