namespace JuTCo.Web.Review.Contracts;

/// <summary>
///     Подсказка к проблеме
/// </summary>
public class HintModel
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
    public HintStyleModel Style { get; set; }

    /// <summary>
    ///     Категория подсказки (ярлык/лейбл)
    /// </summary>
    public HintTabModel Tab { get; set; }
}