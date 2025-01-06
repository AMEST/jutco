using FluentAssertions;
using JuTCo.Text.Review.Detectors;

namespace JuTCo.Text.Review.Tests;

public class WordRepetitionDetectorTests
{
    private readonly WordRepetitionDetector _detector = new();

    [Fact]
    public void Should_Detect_WhenIsWordRepetition()
    {
        const string text = "Привет привет привет. Как как дела?";
        var expected = new[] {"привет привет", "как как"};
        
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