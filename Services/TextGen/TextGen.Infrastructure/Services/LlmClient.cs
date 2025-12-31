using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration; // API Key okumak için
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

public class LlmClient : ILlmClient
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public LlmClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["GEMINI_API_KEY"] ??
                  throw new Exception("GEMINI_API_KEY bulunamadı.");

        _httpClient.BaseAddress = new Uri("https://generativelanguage.googleapis.com/");
    }

    public async Task<T> GenerateContentAsync<T>(string prompt, CancellationToken cancellationToken)
    {
        var requestUrl = $"/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";

        string systemInstruction = "You are a helpful AI assistant that strictly outputs JSON.";

        var payload = new
        {
            system_instruction = new { parts = new[] { new { text = systemInstruction } } },
            contents = new[] { new { role = "user", parts = new[] { new { text = prompt } } } },
            generationConfig = new { response_mime_type = "application/json", temperature = 0.5 } 
        };

        var response = await _httpClient.PostAsJsonAsync(requestUrl, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"LLM Error: {response.StatusCode}. Detay: {err}");
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);

        using var doc = JsonDocument.Parse(responseString);

        // Güvenli erişim (null check eklenebilir ama structure genelde sabittir)
        var textContent = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        if (string.IsNullOrEmpty(textContent))
            throw new Exception("LLM boş içerik döndü.");


        textContent = CleanJsonString(textContent);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            AllowTrailingCommas = true,            // <--- Trailing comma hatasını çözer
            ReadCommentHandling = JsonCommentHandling.Skip // <--- Yorum satırlarını yok sayar
        };

        try
        {
            // 2. Deserialize işlemini bu ayarlarla yapıyoruz
            var result = JsonSerializer.Deserialize<T>(textContent, options);

            if (result == null) throw new Exception("LLM yanıtı modele dönüştürülemedi.");

            return result;
        }
        catch (JsonException ex)
        {
            // 3. Hata alırsan gelen bozuk JSON'ı exception mesajına ekliyoruz ki görebilesin
            throw new InvalidOperationException($"JSON Parse Hatası oluştu. Gelen ham veri: {textContent}", ex);
        }
    }

    // Veri ```json ile başlıyorsa temizle
    private string CleanJsonString(string json)
    {
        if (string.IsNullOrWhiteSpace(json)) return json;

        json = json.Trim();
        if (json.StartsWith("```json"))
        {
            json = json.Substring(7); // ```json kısmını sil
            if (json.EndsWith("```"))
            {
                json = json.Substring(0, json.Length - 3); // Sondaki ``` kısmını at
            }
        }
        else if (json.StartsWith("```")) // Sadece ``` varsa
        {
            json = json.Substring(3);
            if (json.EndsWith("```"))
            {
                json = json.Substring(0, json.Length - 3);
            }
        }
        return json.Trim();
    }

    public async Task<T> PromptTest<T>(string prompt, CancellationToken cancellationToken)
    {
        var requestUrl = $"/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
        var payload = new
        {
            contents = new[] { new { parts = new[] { new { text = prompt } } } }
        }; // "contents" ve "parts" zorunlu veriler

        var response = await _httpClient.PostAsJsonAsync(requestUrl, payload, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var err = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"API Hatası: {response.StatusCode} - {err}");
        }

        var responseString = await response.Content.ReadAsStringAsync(cancellationToken);
        using var doc = JsonDocument.Parse(responseString);

        // Gemini'nin standart JSON yapısından metni çekiyoruz
        var rawText = doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString();

        // Handler içindeki PromptTestResponseModel { Response = ... } yapısına uyması için
        // metni bir JSON objesi gibi simüle edip Deserialize ediyoruz
        var jsonWrapper = JsonSerializer.Serialize(new { Response = rawText });

        return JsonSerializer.Deserialize<T>(jsonWrapper)!;
    }

}
