namespace JuTCo.Core.Domain;

/// <summary>
///     Ярлык/категория подсказки
/// </summary>
public class HintTab
{
    /// <summary>
    ///     Имя категории подсказки
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Цвет категории (в основном должен совпадать с цветом в <see cref="HintStyle"/>)
    /// </summary>
    public string Color { get; set; }
}