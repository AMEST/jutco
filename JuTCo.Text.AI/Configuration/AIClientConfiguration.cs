namespace JuTCo.Text.AI.Configuration;

public class AIClientConfiguration
{
    public string? Endpoint { get; set; }
    public string? ApiKey { get; set; }
    public string ModelName { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public ClientType Type { get; set; } = ClientType.OpenAICompatible;
}