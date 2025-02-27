using JuTCo.Text.AI.Contracts;

namespace JuTCo.Text.AI.Services;

public interface IAIClient
{
    Task<ChatMessage?> CompleteChat(params ChatMessage?[] messages);
}