using JuTCo.Web.Beautifier;
using JuTCo.Web.Beautifier.Contracts;
using JuTCo.Web.Review;
using Microsoft.Extensions.DependencyInjection;

namespace JuTCo.Web;

public static class WebModule
{
    public static IServiceCollection AddWebModule(this IServiceCollection services)
    {
        return services
            .AddSingleton<IReviewAppService, ReviewAppService>()
            .AddSingleton<IBeautifierAppService, BeautifierAppService>();
    }
}