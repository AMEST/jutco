using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор фичеризмов
/// </summary>
internal class FeaturesDetector : IDetector
{
    private const int _weight = 50;
    private const string _name = "Фичеризм";
    private const string _description =
        "Если описываете новые возможности продукта, описывайте их в мире клиента/пользователя";
    private const string _shortDescription = "Если описываете новые возможности продукта, описывайте их в мире клиента/пользователя";
    private const string _color = "orange";
    private const string _tab = "red";

    private static readonly string[] _stopWords =
    [
        "платформу",
        "модуль",
        "технология",
        "модуля",
        "обновление",
        "формата",
        "платформой",
        "технологию",
        "модулем",
        "сервиса",
        "улучшение",
        "версией",
        "синхронизация",
        "возможности",
        "возможностями",
        "функция",
        "дополнение",
        "синхронизации",
        "обновлению",
        //"алгоритма",
        "модулю",
        //"инструменту",
        "дополнением",
        "интеграции",
        //"инструментом",
        "интеграция",
        //"инструмента",
        "опцию",
        "расширению",
        "опция",
        "систему",
        "интерфейс",
        "версию",
        "платформы",
        "функцией",
        "формат",
        "интерфейса",
        "улучшению",
        "система",
        "системами",
        "сервису",
        "платформа",
        "расширение",
        "форматом",
        "технологии",
        "возможность",
        "сервисом",
        //"алгоритм",
        "опции",
        "версия",
        //"алгоритму",
        "интеграцию",
        "сервис",
        "системой",
        "улучшением",
        "улучшения",
        "интеграцией",
        "функцию",
        "дополнению",
        "системе",
        "платформе",
        "обновлением",
        "функции",
        "дополнения",
        "синхронизацией",
        "расширением",
        "интерфейсом",
        "опцией",
        "системы",
        "интерфейсу",
        "версии",
        "технологией",
        "режим",
        "формату",
        "возможностью",
        "режиму",
        "режима",
        "расширения",
        "синхронизацию",
        "обновления",
        //"инструмент",
        //"алгоритмом",
        "режимом",
        "фича",
        "фичу",
        "фичи"
    ];

    private readonly int _minimalWordLength;

    public FeaturesDetector()
    {
        _minimalWordLength =
            _stopWords.Select(x => x.Length).Min();
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