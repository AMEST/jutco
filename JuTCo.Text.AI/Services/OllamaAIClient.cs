using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using JuTCo.Text.AI.Configuration;
using JuTCo.Text.AI.Contracts;
using Microsoft.Extensions.Options;

namespace JuTCo.Text.AI.Services;

internal class OllamaAIClient : IAIClient
{
    private readonly AIClientConfiguration _options;
    private readonly HttpClient _client;
    private readonly JsonSerializerOptions _serializeOptions;

    public OllamaAIClient(IOptions<AIClientConfiguration> options)
    {
        _options = options.Value;
        _client = new HttpClient()
        {
            BaseAddress = new Uri(_options.Endpoint!)
        };
        _serializeOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        if(string.IsNullOrEmpty(_options.Login) || string.IsNullOrEmpty(_options.Password))
            return;
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
            Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_options.Login}:{_options.Password}")));
    }

    public async Task<ChatMessage?> CompleteChat(params ChatMessage?[] messages)
    {
        var request = new OllamaRequest(_options.ModelName, messages);
        var response = await _client.PostAsJsonAsync("/api/chat", request, _serializeOptions);
        response.EnsureSuccessStatusCode();
        var ollamaResponse = await response.Content.ReadFromJsonAsync<OllamaResponse>();
        if(ollamaResponse?.Message is not null && ollamaResponse.Done)
            return ollamaResponse.Message;
        return ChatMessage.CreateSystem("Response error");
    }
}