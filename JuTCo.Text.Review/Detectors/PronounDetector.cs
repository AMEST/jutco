using FuzzySharp;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор местоимений
/// </summary>
/// TODO: Попробовать обобщить к примеру через абстрактный класс
internal class PronounDetector : IDetector
{
    private const int _weight = 0;
    private const string _name = "{0} местоимение";
    private const string _description =
        "Проверьте, можно ли удалить это местоимение без потери смысла";
    private const string _shortDescription = "Проверьте, можно ли удалить это местоимение без потери смысла";
    private const string _color = "orange";
    private const string _tab = "red";

    private static readonly string[] _possessive =
    [
        "мой",
        "твой",
        "его",
        "её",
        "вам",
        "вами",
        "наш",
        "наше",
        "ваш",
        "ваше",
        "вашим",
        "их",
        "им",
        "своих",
        "своим",
        "свой",
        "свои",
        "своё",
        "свое"
    ];
    
    private static readonly string[] _personal =
    [
        "я",
        "ты",
        "он",
        "она",
        "оно",
        "мы",
        "вы",
        "они",
    ];

    private readonly int _minimalWordLength;

    public PronounDetector()
    {
        _minimalWordLength =
            _possessive.Select(x => x.Length).Min();
    }

    public DetectResult DetectSingle(string word)
    {
        if (string.IsNullOrEmpty(word) || word.Length < _minimalWordLength)
            return DetectResult.NotMatch;

        var wordLower = word.ToLowerInvariant();
        var result = Detect(wordLower,"Притяжательные", _possessive);
        if (result.IsMatch)
            return result;
        
        return Detect(wordLower,"Личные", _personal);
    }

    public DetectResult[] DetectAll(string text) => [];

    private static DetectResult Detect(string wordLower, string type, string[] pronouns) =>
        pronouns.Contains(wordLower) ? CreateResult(type) : DetectResult.NotMatch;

    private static DetectResult CreateResult(string type)
    {
        return new DetectResult()
        {
            Name = string.Format(_name, type),
            Description = _description,
            ShortDescription = _shortDescription,
            Color = _color,
            Tab = _tab,
            Weight = _weight
        };
    }
}