using System.ClientModel;
using JuTCo.Text.AI.Configuration;
using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using ChatMessage = JuTCo.Text.AI.Contracts.ChatMessage;

namespace JuTCo.Text.AI.Services;

internal class OpenAICompatibleClient : IAIClient
{
    private readonly AIClientConfiguration _options;
    private readonly OpenAIClient _client;

    public OpenAICompatibleClient(IOptions<AIClientConfiguration> options)
    {
        _options = options.Value;
        _client = _options.Type == ClientType.OpenAI 
            ? new OpenAIClient(_options.ApiKey) 
            : CreateCompatibleClient(_options);
    }

    public async Task<ChatMessage?> CompleteChat(params ChatMessage?[] messages)
    { 
        var chatClient = _client.GetChatClient(_options.ModelName);
        var result = await chatClient.CompleteChatAsync(ToModel(messages));
        return ToEntity(result);
    }

    private static OpenAIClient CreateCompatibleClient(AIClientConfiguration configuration)
    {
        var config = new OpenAIClientOptions()
        {
            Endpoint = new Uri(configuration.Endpoint!)
        };
        return new OpenAIClient(new ApiKeyCredential(configuration.ApiKey ?? "test"), config);
    }

    private static OpenAI.Chat.ChatMessage[] ToModel(params ChatMessage?[] messages)
    {
        return messages.Select<ChatMessage, OpenAI.Chat.ChatMessage>(x =>
            {
                switch (x.Role)
                {
                    case "assistant":
                        return new AssistantChatMessage(x.Content);
                    case "system":
                        return new SystemChatMessage(x.Content);
                    default:
                        return new UserChatMessage(x.Content);
                }
            })
            .ToArray();
    }

    private static ChatMessage? ToEntity(ClientResult<ChatCompletion>? result)
    {
        if (result is null)
            return ChatMessage.CreateSystem("Response error");
        
        var content = result.Value.Content.FirstOrDefault();
        if (content is null)
            return ChatMessage.CreateSystem("Response error");

        return result.Value.Role switch
        {
            ChatMessageRole.Assistant => ChatMessage.CreateAssistant(content.Text),
            ChatMessageRole.System => ChatMessage.CreateSystem(content.Text),
            _ => ChatMessage.CreateUser(content.Text)
        };
    }
}