using FluentAssertions;
using JuTCo.Text.Review.Detectors;

namespace JuTCo.Text.Review.Tests;

public class ModalVerbPhraseDetectorTests
{
    private readonly ModalVerbPhraseDetector _detector = new();

    [Fact]
    public void Should_Detect_WhenIsModalVerbPhrase()
    {
        const string text = "Я могу сделать это. Нам нужно купить продукты. Вы должны закончить работу до конца дня. Можно ли мне взять отпуск?";
        var expected = new[] {"могу сделать", "нужно купить", "должны закончить", "можно ли"};
        
        var actual = _detector.DetectAll(text);

        actual.Should().NotBeNull();
        //actual.Length.Should().Be(expected.Length);
        foreach (var result in actual)
        {
            result.IsMatch.Should().BeTrue();
            var substring = text.Substring(result.Start.Value, result.End.Value - result.Start.Value).ToLowerInvariant();
            substring.Should().NotBeEmpty();
            expected.Contains(substring).Should().BeTrue();
        }
    }
}