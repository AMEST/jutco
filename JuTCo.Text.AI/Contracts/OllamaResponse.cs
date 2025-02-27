using System.Text.Json.Serialization;

namespace JuTCo.Text.AI.Contracts;

public class OllamaResponse
{
    public string Model { get; set; }
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
    public ChatMessage? Message { get; set; }
    public bool Done { get; set; }
    [JsonPropertyName("total_duration")]
    public long TotalDuration { get; set; }
}