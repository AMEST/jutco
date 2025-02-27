using JuTCo.Core.Services;
using JuTCo.Text.AI.Contracts;

namespace JuTCo.Text.AI.Services;

internal class TextBeautifierService : IAITextProcessingService
{
    private readonly IAIClient _aiClient;

    public TextBeautifierService(IAIClient aiClient)
    {
        _aiClient = aiClient;
    }
    
    public Task<string> TextCorrection(string text) => Execute(PromptCollection.TextCorrection, text);

    public Task<string> TextEmojination(string text) => Execute(PromptCollection.Emojination, text);

    public Task<string> TextBeautifier(string text, string? additionalPrompt = null) =>
        Execute($"{PromptCollection.TextCorrection}\n{additionalPrompt ?? string.Empty}", text);

    private async Task<string> Execute(string prompt, string text)
    {
        var promptMessage = ChatMessage.CreateSystem(prompt);
        var userMessage = ChatMessage.CreateUser(text);

        var result = await _aiClient.CompleteChat(promptMessage, userMessage);
        return result?.Content ?? string.Empty;
    }
}