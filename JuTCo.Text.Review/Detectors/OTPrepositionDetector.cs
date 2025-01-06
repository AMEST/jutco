using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор предлога ОТ
/// </summary>
internal class OTPrepositionDetector : IDetector
{
    private const int _weight = 30;
    private const string _name = "Предлог 'от'";
    private const string _description =
        "Замените на родительный падеж без предлога, если обозначает принадлежность";
    private const string _shortDescription = "Замените на родительный падеж без предлога, если обозначает принадлежностьи";
    private const string _color = "orange";
    private const string _tab = "red";

    public DetectResult DetectSingle(string word)
    {
        if (string.IsNullOrEmpty(word))
            return DetectResult.NotMatch;

        var wordLower = word.ToLowerInvariant();
        return wordLower == "от" ? CreateResult() : DetectResult.NotMatch;
    }

    public DetectResult[] DetectAll(string text) => [];

    private static DetectResult CreateResult()
    {
        return new DetectResult()
        {
            Name = _name,
            Description = _description,
            ShortDescription = _shortDescription,
            Color = _color,
            Tab = _tab,
            Weight = _weight
        };
    }
}