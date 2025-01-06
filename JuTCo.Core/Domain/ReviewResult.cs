namespace JuTCo.Core.Domain;

/// <summary>
///     Результат ревью текста
/// </summary>
public class ReviewResult
{
    /// <summary>
    ///     Количество баллов у текста
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    ///     Список проблем
    /// </summary>
    public Fragment[] Fragments { get; set; } = Array.Empty<Fragment>();

    public static ReviewResult Empty => new ReviewResult() { Score = 10 };
}