namespace JuTCo.Core.Domain;

/// <summary>
///     Подсказка к проблеме
/// </summary>
public class Hint
{
    /// <summary>
    ///     Имя подсказки
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Краткое описание подсказки
    /// </summary>
    public string ShortDescription { get; set; }
    
    /// <summary>
    ///     Описание подсказки (объяснение)
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    ///     Стиль подсказки
    /// </summary>
    public HintStyle Style { get; set; }

    /// <summary>
    ///     Категория подсказки (ярлык/лейбл)
    /// </summary>
    public HintTab Tab { get; set; }
}