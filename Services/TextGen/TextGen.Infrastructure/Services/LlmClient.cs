using System;
using System.Collections.Generic;
using System.Text;
using TextGen.Application.Models;
using TextGen.Application.Services;

namespace TextGen.Infrastructure.Services;

public class LlmClient : ILlmClient
{
    public async Task<LlmTextResponseModel> GenerateTextAsync(string prompt, CancellationToken cancellationToken)
    {
        // 1. Gerçekçi olması için sunucu gecikmesi simülasyonu (1.5 saniye)
        await Task.Delay(100, cancellationToken);

        // 2. Geçici (Dummy) veri üretimi
        // İstersen prompt'u da içeriğe ekleyerek debug yapmayı kolaylaştırabilirsin.
        var mockContent = $@"
        Bu, henüz LLM servisi bağlanmadığı için oluşturulmuş otomatik bir metindir. 
        Sistem başarıyla çalışıyor! 
        
        Gelen Prompt Bilgisi: {prompt.Substring(0, Math.Min(prompt.Length, 50))}...
        
        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";

        return new LlmTextResponseModel
        {
            Title = "Otomatik Oluşturulan Test Başlığı",
            Content = mockContent,
            WordCount = mockContent.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length
        };
    }
}
