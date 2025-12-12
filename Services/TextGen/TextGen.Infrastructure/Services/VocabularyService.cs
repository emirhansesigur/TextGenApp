using System.Net.Http.Json;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

public class VocabularyService(HttpClient _httpClient) : IVocabularyService
{
    public async Task<UserWordListDto?> GetUserWordListAsync(Guid id, CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync($"api/UserWordLists/{id}" ,cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<UserWordListDto>(cancellationToken: cancellationToken);
    }
}