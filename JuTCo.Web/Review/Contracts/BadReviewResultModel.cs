namespace JuTCo.Web.Review.Contracts;

public class BadReviewResultModel
{
    /// <summary>
    ///     Статус проведения ревью
    /// </summary>
    public string Status { get; } = "error";

    /// <summary>
    ///     Код ошибки
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    ///     Сообщение к ошибке
    /// </summary>
    public string Message { get; set; }
}