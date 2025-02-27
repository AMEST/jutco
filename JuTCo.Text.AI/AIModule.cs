using JuTCo.Core.Services;
using JuTCo.Text.AI.Configuration;
using JuTCo.Text.AI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace JuTCo.Text.AI;

public static class AIModule
{
    public static IServiceCollection AddAIModule(this IServiceCollection services)
    {
        return services.AddSingleton<IAIClient>(CreateClient)
            .AddSingleton<IAITextProcessingService, TextBeautifierService>();
    }

    private static IAIClient CreateClient(IServiceProvider provider)
    {
        var configuration = provider.GetRequiredService<IOptions<AIClientConfiguration>>();
        return configuration.Value.Type switch
        {
            ClientType.OpenAI => new OpenAICompatibleClient(configuration),
            ClientType.OpenAICompatible => new OpenAICompatibleClient(configuration),
            ClientType.Ollama => new OllamaAIClient(configuration),
            ClientType.NoOp => new NoOpAIClient(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}