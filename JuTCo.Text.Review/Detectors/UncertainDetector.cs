using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор неопределенного
/// </summary>
internal class UncertainDetector : IDetector
{
    private const int _weight = 100;
    private const string _name = "Неопределеные";
    private const string _description =
        "Неопределенные слова разбавляют текст не добавляя смысла. Они вызывают настороженность. Попробуйте добавить определенности";
    private const string _shortDescription = "Попробуйте добавить определенности";
    private const string _color = "orange";
    private const string _tab = "red";

    private static readonly string[] _stopWords =
    [
        "какие-то",
        "какую-то",
        "какое-то",
        "какой-то",
        "когда-то",
        "где-то",
        "как-то",
        "чего-то",
        "что-то",
        "зачем-то",
        "почему-то",
        "всякие",
        "разные",
        "разных",
        "разное",
        "почти",
        "некоторые",
        "того",
        "иного",
        "иногда",
        "всякий",
        "всякое",
        "всяческий",
        "другой",
        "нечто",
        "некто",
        "несколько",
        "зачем-либо",
        "кое-что",
        "кое-кто",
        "кто-нибудь",
        "примерно",
        "около",
        "порядка",
        "всей",
        "всего",
        "достаточно",
        "достаточный",
        "часто",
        "чаще",
        "множество",
        "многих",
        "различными",
        "различных",
        "различные",
        "различное",
        "одной"
    ];

    private static readonly string[] _complexStopWords = new[]
    {
        "одна из главных",
        "одно из главных",
        "одни из главных",
    };

    private readonly int _minimalWordLength;

    public UncertainDetector()
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

    public DetectResult[] DetectAll(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length < _minimalWordLength)
            return [];

        var result = new List<DetectResult>();
        foreach (var phrase in _complexStopWords)
        {
            var index = text.IndexOf(phrase, StringComparison.OrdinalIgnoreCase);
            if (index == -1)
                continue;
            result.Add(new DetectResult()
            {
                Start = index,
                End = index + phrase.Length,
                Name = _name,
                Description = _description,
                ShortDescription = _shortDescription,
                Color = _color,
                Tab = _tab,
                Weight = _weight
            });
        }

        return result.ToArray();
    }

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