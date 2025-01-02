namespace JuTCo.Web.Review.Contracts;

/// <summary>
///     Результат ревью текста
/// </summary>
public class ReviewResultModel
{
    /// <summary>
    ///     Количество баллов у текста
    /// </summary>
    public double Score { get; set; }

    /// <summary>
    ///     Список проблем
    /// </summary>
    public FragmentModel[] Fragments { get; set; }

    /// <summary>
    ///     Статус проведения ревью
    /// </summary>
    public string Status { get; } = "ok";

    /// <summary>
    ///     Код ошибки (если status != ok)
    /// </summary>
    public string Code { get; private set; }

    /// <summary>
    ///     Сообщение к ошибке (если status != ok)
    /// </summary>
    public string Message { get; private set; }
}