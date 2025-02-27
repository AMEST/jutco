using JuTCo.Text.AI.Contracts;

namespace JuTCo.Text.AI.Services;

public class NoOpAIClient : IAIClient
{
    public Task<ChatMessage?> CompleteChat(params ChatMessage?[] messages)
    {
        return Task.FromResult<ChatMessage>(null);
    }
}