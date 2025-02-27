namespace JuTCo.Core.Services;

/// <summary>
///     Сервис улучшения текстов
/// </summary>
public interface IAITextProcessingService
{
    /// <summary>
    ///     Восстановление пунктуации и грамматики в тексте
    /// </summary>
    Task<string> TextCorrection(string text);
    
    /// <summary>
    ///     Добавление "живости" текста через добавление emoji по смыслу
    /// </summary>
    Task<string> TextEmojination(string text);
    
    Task<string> TextBeautifier(string text, string? additionalPrompt = null);
}