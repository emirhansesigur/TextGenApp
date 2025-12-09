using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace TextGen.Infrastructure.Services;

public class VocabularyService //: IVocabularyService
{
    //private readonly HttpClient _httpClient;

    //public VocabularyService(HttpClient httpClient)
    //{
    //    _httpClient = httpClient;
    //}

    //public async Task<List<UserWordListDto>> GetUserWordListsAsync(string userId)
    //{
    //    // Vocabulary servisine (Port 5001) istek atıyoruz.
    //    // Not: Adresi Program.cs'de tanımlayacağımız için buraya sadece endpoint yolunu yazıyoruz.
    //    // "UserWordLists/byUser" kısmı senin API endpoint yolun olmalı.
    //    var response = await _httpClient.GetAsync($"api/UserWordLists/byUser?userId={userId}");

    //    if (response.IsSuccessStatusCode)
    //    {
    //        // Gelen JSON'ı otomatik olarak DTO'ya çeviriyoruz.
    //        return await response.Content.ReadFromJsonAsync<List<UserWordListDto>>();
    //    }

    //    // Hata durumunda boş liste dönebilir veya hata fırlatabilirsin.
    //    return new List<UserWordListDto>();
    //}
}