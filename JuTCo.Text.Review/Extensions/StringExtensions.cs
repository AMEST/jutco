namespace JuTCo.Text.Review.Extensions;

public static class StringExtensions
{
    public static bool CheckSimilarityByLength(this string first, string second)
    {
        return Math.Abs(first.Length - second.Length) < 2;
    }

    public static int CountWords(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;
        return text
            .Replace("\n", "")
            .Split(" ")
            .Select(x => x.Trim())
            .Count(x => x.Length > 1);
    }
}