namespace JuTCo.Web.Host.Extensions;

internal static class ConfigurationExtensions
{
    public static bool IsSwaggerEnabled(this IConfiguration configuration)
    {
        return configuration.GetSection("Swagger").GetValue<bool>("Enabled", false);
    }
}