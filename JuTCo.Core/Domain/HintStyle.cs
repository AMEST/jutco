namespace JuTCo.Core.Domain;

/// <summary>
///     Стиль подсказки
/// </summary>
public class HintStyle
{
    /// <summary>
    ///     Название стиля (может совпадать с цветом)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Цвет подсказки
    /// </summary>
    public string Color { get; set; }

    /// <summary>
    ///     Имя Css класса (если есть и нужен)
    /// </summary>
    public string CssName { get; set; }
}