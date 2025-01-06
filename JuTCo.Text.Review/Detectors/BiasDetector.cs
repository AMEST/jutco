using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор необъективной оценки
/// </summary>
/// TODO: Попробовать обобщить к примеру через абстрактный класс
internal class BiasDetector : IDetector
{
    private const int _weight = 100;
    private const string _name = "Необъективная оценка";
    private const string _description =
        "Необоснованную оценку лучше доказывать фактами, если нет 'пруфов' то, лучше удалить из текста";
    private const string _shortDescription = "Лучше удалить или приложить факты";
    private const string _color = "red";
    private const string _tab = "red";

    private static readonly string[] _stopWords =
    [
        "успешный",
        "успешно",
        "низкий",
        "низкое",
        "активный",
        "активно",
        "активное",
        "лучший",
        "удобно",
        "удобное",
        "удобная",
        "удобный",
        "удобнее",
        "широко",
        "по-настоящему",
        "эффективный",
        "качественный",
        "надёжный",
        "превосходный",
        "великолепный",
        "замечательный",
        "выдающийся",
        "исключительный",
        "феноменальный",
        "потрясающий",
        "грандиозный",
        "эпический",
        "легендарный",
        "культовый",
        "популярный",
        "модный",
        "актуальный",
        "востребованный",
        "перспективный",
        "многообещающий",
        "инновационный",
        "революционный",
        "прорывной",
        "уникальный",
        "неповторимый",
        "оригинальный",
        "креативный",
        "быстр",
        "быстрей",
        "быстрой",
        "быстрейший",
        "обычный",
        "обычное",
        "обычные",
        "обычно",
        "просто",
        "простое",
        "простой",
        "простейший",
    ];

    private readonly int _minimalWordLength;

    public BiasDetector()
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