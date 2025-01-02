namespace JuTCo.Core.Services;

/// <summary>
///     Сервис улучшения текстов
/// </summary>
public interface ITextBeautifierService
{
    /// <summary>
    ///     Восстановление пунктуации и грамматики в тексте
    /// </summary>
    Task<string> RepairGrammarAndPunctuation(string text);

    /// <summary>
    ///     Добавление "живости" текста через добавление emoji по смыслу
    /// </summary>
    Task<string> TextEmojination(string text);
}