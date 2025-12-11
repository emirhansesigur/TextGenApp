using System.Net.Http.Json;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

public class VocabularyService(HttpClient _httpClient) : IVocabularyService
{
    public async Task<List<UserWordListDto>> GetUserWordListsAsync()
    {
        var response = await _httpClient.GetAsync($"api/UserWordLists/byUser");

        if (response.IsSuccessStatusCode)
        {
            // Gelen JSON'ı otomatik olarak DTO'ya çeviriyoruz.
            return await response.Content.ReadFromJsonAsync<List<UserWordListDto>>();
        }

        // Hata durumunda boş liste dönebilir veya hata fırlatabilirsin.
        return new List<UserWordListDto>();
    }

    public async Task<UserWordListDto> GetUserWordAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"api/UserWordLists/{id}");
        if (response.IsSuccessStatusCode)
        {
            // Gelen JSON'ı otomatik olarak DTO'ya çeviriyoruz.
            return await response.Content.ReadFromJsonAsync<UserWordListDto>();
        }

        // Hata durumunda boş liste dönebilir veya hata fırlatabilirsin.
        return new UserWordListDto();
    }
}