using FluentAssertions;
using JuTCo.Text.Review.Detectors;

namespace JuTCo.Text.Review.Tests;

public class StampClicheDetectorTests
{
    private readonly StampClicheDetector _detector = new();

    [Fact]
    public void Should_Detect_WhenIsStampOrCliche()
    {
        var text = """
                    Текст тестовый, в котором самый широкий спектр клише и штампов в этом проекте.
                    Таким образом, можно проверить детектор этого проекта причем вполне неплохо на сегодняшний день.
                   """;
        var expected = new[] {"самый широкий спектр", "таким образом", "на сегодняшний день"};
        
        var actual = _detector.DetectAll(text);

        actual.Should().NotBeNull();
        actual.Length.Should().Be(expected.Length);
        foreach (var result in actual)
        {
            result.IsMatch.Should().BeTrue();
            var substring = text.Substring(result.Start.Value, result.End.Value - result.Start.Value).ToLowerInvariant();
            substring.Should().NotBeEmpty();
            expected.Contains(substring).Should().BeTrue();
        }
    }
}