using JuTCo.Core.Domain;

namespace JuTCo.Core.Services;

/// <summary>
///     Сервис проведения ревью текстов
/// </summary>
public interface ITextReviewService
{
    /// <summary>
    ///     Проведение ревью текста с подсказками по исправлению
    /// </summary>
    Task<ReviewResult> Review(string text);
}