using System.Net.Http.Json;
using TextGen.Application.Models.DataTransfer;
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

public class VocabularyService(HttpClient _httpClient) : IVocabularyService
{
    public async Task<List<UserWordListDto>> GetUserWordListsAsync()
    {
        // Vocabulary servisine (Port 5001) istek atıyoruz.
        // Not: Adresi Program.cs'de tanımlayacağımız için buraya sadece endpoint yolunu yazıyoruz.
        // "UserWordLists/byUser" kısmı senin API endpoint yolun olmalı.
        var response = await _httpClient.GetAsync($"api/UserWordLists/byUser");

        if (response.IsSuccessStatusCode)
        {
            // Gelen JSON'ı otomatik olarak DTO'ya çeviriyoruz.
            return await response.Content.ReadFromJsonAsync<List<UserWordListDto>>();
        }

        // Hata durumunda boş liste dönebilir veya hata fırlatabilirsin.
        return new List<UserWordListDto>();
    }
}