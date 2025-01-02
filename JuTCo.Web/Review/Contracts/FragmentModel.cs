namespace JuTCo.Web.Review.Contracts;

/// <summary>
///     Фрагмент текста с проблемой
/// </summary>
public class FragmentModel
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
    public HintModel Hint { get; set; }
    
}