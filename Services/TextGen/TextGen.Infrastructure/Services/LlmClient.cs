using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration; // API Key okumak için
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

// LLM'den gelen JSON'u temsil eder. 
public class LlmApiContentPart
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("wordCount")]
    public int WordCount { get; set; }

    // KeywordsUsed alanı, bu örnekte kullanılmasa da LLM'den gelecektir.
    [JsonPropertyName("keywordsUsed")]
    public List<string> KeywordsUsed { get; set; } = new List<string>();
}

public class LlmClient : ILlmClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    // HttpClient ve IConfiguration'ı DI üzerinden al
    public LlmClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;

        _apiKey = configuration["GEMINI_API_KEY"] ??
                  throw new Exception("GEMINI_API_KEY bulunamadı. Lütfen .env dosyasını kontrol edin.");

        // Base URL'i bir kere tanımlayabiliriz.
        _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
    }

    public async Task<LlmTextResponseModel> GenerateTextAsync(string prompt, CancellationToken cancellationToken)
    {
        var requestUrl = $"/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

        // 1. System Instruction: Modelin kimliği (Bunu da config'den veya dosyadan alabilirsin)
        var systemInstructionText = "You are a professional English Language Teacher (ELT) AI. You strictly output JSON.";

        // 2. Payload Yapısı (Gemini System Instruction Formatı)
        var payload = new
        {
            system_instruction = new
            {
                parts = new[] { new { text = systemInstructionText } }
            },
            contents = new[]
            {
                new { role = "user", parts = new[] { new { text = prompt } } }
            },
            generationConfig = new
            {
                response_mime_type = "application/json",
                temperature = 0.7 // Yaratıcılık seviyesi (0.0 - 1.0). 0.7 dengelidir.
            }
        };

        var response = await _httpClient.PostAsJsonAsync(requestUrl, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"LLM API'den hata döndü. Status: {response.StatusCode}. Detay: {errorContent}");
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        // Gelen yanıt (responseString) aşağıdaki gibi bir yapıdır:
        /*
        {
          "candidates": [
            {
              "content": {
                "parts": [
                  {
                    "text": "{\n  \"title\": \"...",\n  \"content\": \"...\",\n  \"wordCount\": 150,\n  \"keywordsUsed\": [\"...\"]\n}"
                  }
                ]
              }
            }
          ]
        }
        */

        // İç içe JSON yapısını ayrıştır
        using var doc = JsonDocument.Parse(responseString);

        // Asıl metin (yani PromptBuilder'dan istediğimiz JSON) en derin katmanda:
        var textContent = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        // LLM'den aldığımız saf JSON string'i, kendi LlmTextResponseModel'imize çeviriyoruz.
        var result = JsonSerializer.Deserialize<LlmApiContentPart>(textContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        // Uygulama katmanının beklediği modele (LlmTextResponseModel) dönüştür
        return new LlmTextResponseModel
        {
            Title = result?.Title ?? "Başlık Yok",
            Content = result?.Content ?? "İçerik Yok",
            WordCount = result?.WordCount ?? 0
        };
    }
}