using JuTCo.Text.Review.Contracts;

namespace JuTCo.Text.Review.Detectors;

public interface IDetector
{
    /// <summary>
    ///     Определение проблемы над одним словом
    /// </summary>
    /// <remarks>Подходит для пошагового прохода по тексту, для проверки каждого отдельно взятого слова</remarks>
    /// <param name="word">Одно слово</param>
    DetectResult DetectSingle(string word);

    /// <summary>
    ///     Определение проблем во всем тексте
    /// </summary>
    /// <remarks>Подходит для проверок повторяющихся слов или для словосочетаний (для правил где больше одного "слова" в правиле проверки)</remarks>
    /// <param name="text">Входящий текст</param>
    DetectResult[] DetectAll(string text);
}