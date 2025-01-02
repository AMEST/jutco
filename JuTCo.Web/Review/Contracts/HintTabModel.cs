namespace JuTCo.Web.Review.Contracts;

/// <summary>
///     Ярлык/категория подсказки
/// </summary>
public class HintTabModel
{
    /// <summary>
    ///     Имя категории подсказки
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Цвет категории (в основном должен совпадать с цветом в <see cref="HintStyleModel"/>)
    /// </summary>
    public string Color { get; set; }
}