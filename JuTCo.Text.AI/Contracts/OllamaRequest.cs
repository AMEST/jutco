namespace JuTCo.Text.AI.Contracts;

public class OllamaRequest(string model, ChatMessage?[] messages)
{
    public bool Stream { get; set; } = false;
    public string Model { get; set; } = model;
    public ChatMessage?[] Messages { get; set; } = messages;
}