using System.Text.Json.Serialization;

namespace TextGen.Application.Models.Llm;

public class VocabularyValidationResponse
{
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = string.Empty;

    [JsonPropertyName("approvedWords")]
    public List<string> ApprovedWords { get; set; } = new();

    [JsonPropertyName("rejectedWords")]
    public List<string> RejectedWords { get; set; } = new();
}