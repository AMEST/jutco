using System.Text.RegularExpressions;
using JuTCo.Text.Review.Contracts;

namespace JuTCo.Text.Review.Detectors;

/// <summary>
///     Детектор паразитов времени
/// </summary>
internal class TimeParasiteDetector : IDetector
{
    private const int _weight = 100;
    private const string _name = "Паразиты времени";

    private const string _description =
        "Попробуйте убрать, уточните или противопоставьте прошлому или будущему";

    private const string _shortDescription = "Попробуйте убрать, уточните или противопоставьте прошлому или будущему";
    private const string _color = "red";
    private const string _tab = "red";
    
    private static readonly string[] _parasites = { 
        "На текущий момент",
        "на текущий момент",
        "В настоящее время", 
        "в настоящее время", 
        "настоящий момент", 
        "В настоящий момент", 
        "в настоящий момент", 
        "Настоящий момент", 
        "В наши дни",
        "в наши дни",
        "В последнее время", 
        "в последнее время", 
        "В современной России", 
        "в современной России", 
        "В современном мире", 
        "в современном мире", 
        "В последние годы",
        "В последний год",
        "в последние годы",
        "в последний год",
        "В последние месяцы",
        "в последние месяцы",
        "В последний месяц",
        "в последний месяц",
        "В последние недели",
        "в последние недели",
        "В последнюю неделю",
        "в последнюю неделю",
        "В последние дни",
        "в последние дни",
        "В последний день",
        "в последний день",
        "Нынче", 
        "нынче",
        "Сейчас", 
        "сейчас", 
        "До недавнего времени",
        "до недавнего времени" 
    };

    private static readonly Regex _timeParasitesRegex =
        new Regex(@"(?:\n|^|\.|\!|\?)\s*(" + string.Join("|", _parasites) + ")", RegexOptions.Compiled & RegexOptions.IgnoreCase);


    public DetectResult DetectSingle(string word) => DetectResult.NotMatch;

    public DetectResult[] DetectAll(string text)
    {
        if (string.IsNullOrWhiteSpace(text) || text.Length < 10)
            return [];

        var result = new List<DetectResult>();
        var matches = _timeParasitesRegex.Matches(text);
        
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