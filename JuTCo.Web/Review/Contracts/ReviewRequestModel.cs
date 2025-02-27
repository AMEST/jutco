using System.ComponentModel.DataAnnotations;

namespace JuTCo.Web.Review.Contracts;

/// <summary>
///     Запрос на проведение ревью
/// </summary>
public class ReviewRequestModel
{
    /// <summary>
    ///     Текст для провеения ревью
    /// </summary>
    [Required]
    public string Text { get; set; }
}