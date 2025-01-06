using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор обобщений
/// </summary>
internal class GeneralizationDetector : IDetector
{
    private const int _weight = 100;
    private const string _name = "Обобщения";
    private const string _description =
        "Попробуйте удалить обобщение, скорее всего смысл текста не пострадает";
    private const string _shortDescription = "Попробуйте удалить обобщение, скорее всего смысл текста не пострадает";
    private const string _color = "orange";
    private const string _tab = "red";

    private static readonly string[] _stopWords =
    [
        "более",
        "вообще",
        "всегда",
        "всюду",
        "везде",
        "никогда",
        "любой",
        "всякий",
        "каждый",
        "весь",
        "целый",
        "целую",
        "целые",
        "все",
        "вся"
    ];

    private readonly int _minimalWordLength;

    public GeneralizationDetector()
    {
        _minimalWordLength =
            _stopWords.Select(x => x.Length).Min() - 2; // Вычитаем 2 символа чтобы учесть слова с ошибками
    }

    public DetectResult DetectSingle(string word)
    {
        if (string.IsNullOrEmpty(word) || word.Length < _minimalWordLength)
            return DetectResult.NotMatch;

        var wordLower = word.ToLowerInvariant();
        if (_stopWords.Contains(wordLower))
            return CreateResult();

        var findResult = _stopWords
            .ToDictionary(x => x, x => Fuzz.PartialRatio(wordLower, x))
            .MaxBy(x => x.Value);
        if (findResult.Value < 88 || !findResult.Key.CheckSimilarityByLength(wordLower))
            return DetectResult.NotMatch;

        return CreateResult();
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