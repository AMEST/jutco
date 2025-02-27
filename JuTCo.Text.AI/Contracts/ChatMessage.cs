namespace JuTCo.Text.AI.Contracts;

public class ChatMessage(string role, string content)
{
    public string Role { get; set; } = role;
    public string Content { get; set; } = content;

    public static ChatMessage? CreateUser(string content) => new ChatMessage("user", content);
    public static ChatMessage? CreateAssistant(string content) => new ChatMessage("assistant", content);
    public static ChatMessage? CreateSystem(string content) => new ChatMessage("system", content);
}