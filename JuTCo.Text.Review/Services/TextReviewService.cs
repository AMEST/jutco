using System.Text.RegularExpressions;
using JuTCo.Core.Domain;
using JuTCo.Core.Services;
using JuTCo.Text.Review.Contracts;
using JuTCo.Text.Review.Detectors;
using JuTCo.Text.Review.Extensions;

namespace JuTCo.Text.Review.Services;

internal partial class TextReviewService : ITextReviewService
{
    private readonly IEnumerable<IDetector> _detectors;

    public TextReviewService(IEnumerable<IDetector> detectors)
    {
        _detectors = detectors;
    }
    
    public Task<ReviewResult> Review(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return Task.FromResult(ReviewResult.Empty);
        
        var results = new List<DetectResult>();
        foreach (var detector in _detectors)
            results.AddRange(detector.DetectAll(text));
        results.AddRange(DetectSingleWords(text));

        if (results.Count == 0)
            return Task.FromResult(ReviewResult.Empty);

        var countWords = text.CountWords();
        var score = results.Count > 0
            ? CalculateScore(results, countWords)
            : 10.0; // 10 бальная система

        return Task.FromResult(new ReviewResult()
        {
            Score = score,
            Fragments = results.Select(x => x.ToFragment()).ToArray()
        });
    }

    private IList<DetectResult> DetectSingleWords(string text)
    {
        var results = new List<DetectResult>();
        foreach (Match match in WordDetectRegex().Matches(text))
        {
            var word = match.Value;
            var startIndex = match.Index;
            var endIndex = match.Index + match.Length;

            var detectResults = _detectors
                .Select(x => x.DetectSingle(word))
                .Where(x => x.IsMatch)
                .ToArray();
            foreach (var detectResult in detectResults)
            {
                detectResult.Start = startIndex;
                detectResult.End = endIndex;
            }
            results.AddRange(detectResults);
        }

        return results;
    }

    private static double CalculateScore(IList<DetectResult> results, int wordsCount)
    {
        var detectionsWeight = 0.0;
        foreach (var result in results)
            detectionsWeight += result.Weight / 100.0;
        return Math.Floor(100 * Math.Pow(1.0 - detectionsWeight / (double) wordsCount, 3)) / 10.0;
    }

    [GeneratedRegex(@"[a-zA-Zа-яА-ЯёЁ0-9-]+", RegexOptions.Compiled)]
    private static partial Regex WordDetectRegex();
}