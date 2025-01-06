using System.Text.RegularExpressions;
using JuTCo.Text.Review.Contracts;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор штампов и клише
/// </summary>
internal class ModalVerbPhraseDetector : IDetector
{
    private const int _weight = 80;
    private const string _name = "Фраза с модальным глаголом";

    private const string _description =
        "Попробуйте убрать модальный, оставьте смысловой глагол";

    private const string _shortDescription = "Попробуйте убрать модальный, оставьте смысловой глагол";
    private const string _color = "blue";
    private const string _tab = "red";

    private static readonly string[] _modalVerbs =
    [
        "мочь",
        "могу",
        "могут",
        "хотеть",
        "хотят",
        "хочет",
        "надо",
        "нужно",
        "следует",
        "смогут",
        "можно",
        "может",
        "можете",
        "можем",
        "можно",
        "нельзя",
        "должны",
        "должен",
        "обязан",
        "обязаны",
    ];
    private static readonly Regex _modalVerbsPhrasesRegex = new Regex(@"\b(" + string.Join("|", _modalVerbs) + @")\b\s+(\w+)\b", RegexOptions.IgnoreCase);
    
    private readonly int _minimalPhraseLength;

    public ModalVerbPhraseDetector()
    {
        _minimalPhraseLength = _modalVerbs.Select(x => x.Length).Min();
    }

    public DetectResult DetectSingle(string word) => DetectResult.NotMatch;

    public DetectResult[] DetectAll(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length < _minimalPhraseLength)
            return [];

        var result = new List<DetectResult>();
        var matches = _modalVerbsPhrasesRegex.Matches(text);
        
        foreach (Match match in matches)
        {
            var startIndex = match.Index;
            var endIndex = match.Index + match.Length;

            result.Add(new DetectResult()
            {
                Start = startIndex,
                End = endIndex,
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
}