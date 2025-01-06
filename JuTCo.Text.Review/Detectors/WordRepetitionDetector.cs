using System.Text.RegularExpressions;
using JuTCo.Text.Review.Contracts;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор повторений слов
/// </summary>
internal partial class WordRepetitionDetector : IDetector
{
    private const int _weight = 0;
    private const string _name = "Повтор";

    private const string _description =
        "Повтор слова, проверьте, не ошибка ли";

    private const string _shortDescription = "Повтор слова, проверьте, не ошибка ли";
    private const string _color = "blue";
    private const string _tab = "blue";

    public DetectResult DetectSingle(string word) => DetectResult.NotMatch;

    public DetectResult[] DetectAll(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length < 10)
            return [];

        var result = new List<DetectResult>();
        var matches = WordRepetitionRegex().Matches(text);
        
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

    [GeneratedRegex(@"\b(\b\w+\b)\s+\1\b", RegexOptions.Compiled | RegexOptions.IgnoreCase)]
    private static partial Regex WordRepetitionRegex();
}