using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор усилителей оценки
/// </summary>
/// TODO: Попробовать обобщить к примеру через абстрактный класс
internal class AmplifierDetector : IDetector
{
    private const int _weight = 100;
    private const string _name = "Усилители";

    private const string _description =
        "Такими словами пытаются усилить оценку. Но сами по себе без докозательств, не убеждает читателя. Подкрепите текст докозательствами, чтобы оценка и усилитель не были голословными";

    private const string _shortDescription =
        "Попытка усиления оценки, без докозательств - не убедит читателя";

    private const string _color = "red";
    private const string _tab = "red";

    private static readonly string[] _stopWords =
    [
        "очень",
        "прямо",
        "максимально",
        "абсолютно",
        "предельно",
        "сильно",
        "наиболее",
        "самый",
        "самая",
        "самое",
        "самые",
        "чрезвычайно",
        "крайне",
        "весьма",
        "исключительно",
        "необыкновенно",
        "неимоверно",
        "невыразимо",
        "несказанно",
        "безмерно",
        "безгранично",
        "беспредельно",
        "неописуемо",
        "невообразимо",
        "фантастически",
        "феноменально",
        "колоссально",
        "грандиозно",
        "эпично",
        "гигантски",
        "чудовищно",
        "лишь",
        "особо",
        "особенный",
        "особенное",
        "особый",
        "именно"
    ];

    private readonly int _minimalWordLength;

    public AmplifierDetector()
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
        if (findResult.Value < 88|| !findResult.Key.CheckSimilarityByLength(wordLower))
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