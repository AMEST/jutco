namespace JuTCo.Core.Domain;

/// <summary>
///     Фрагмент текста с проблемой
/// </summary>
public class Fragment
{
    /// <summary>
    ///     Начало выделения текста
    /// </summary>
    public int Start { get; set; }
    
    /// <summary>
    ///     Конец выделения текста
    /// </summary>
    public int End { get; set; }
    
    /// <summary>
    ///     Подсказка по проблеме
    /// </summary>
    public Hint Hint { get; set; }
    
}