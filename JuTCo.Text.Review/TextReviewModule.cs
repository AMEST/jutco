using System.Runtime.CompilerServices;
using JuTCo.Core.Services;
using JuTCo.Text.Review.Detectors;
using JuTCo.Text.Review.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly:InternalsVisibleTo("JuTCo.Text.Review.Tests")]
namespace JuTCo.Text.Review;

public static class TextReviewModule
{
    public static IServiceCollection AddTextReviewModule(this IServiceCollection services)
    {
        return services
            .AddSingleton<IDetector, UncertainDetector>()
            .AddSingleton<IDetector, ChancelleryDetector>()
            .AddSingleton<IDetector, StampClicheDetector>()
            .AddSingleton<IDetector, BiasDetector>()
            .AddSingleton<IDetector, AmplifierDetector>()
            .AddSingleton<IDetector, PronounDetector>()
            .AddSingleton<IDetector, ModalVerbPhraseDetector>()
            .AddSingleton<IDetector, WordRepetitionDetector>()
            .AddSingleton<IDetector, TimeParasiteDetector>()
            .AddSingleton<IDetector, GeneralizationDetector>()
            .AddSingleton<IDetector, FeaturesDetector>()
            .AddSingleton<ITextReviewService, TextReviewService>();
    }
}