using FluentAssertions;
using JuTCo.Text.Review.Detectors;

namespace JuTCo.Text.Review.Tests;

public class UncertainDetectorTests
{
    private readonly UncertainDetector _detector = new();

    [Theory]
    [InlineData("какой-то")]
    [InlineData("Всякие")]
    [InlineData("Всяческие")]
    [InlineData("Некто")]
    [InlineData("всякое")]
    public void Should_Detect_WhenIsUncertain(string word)
    {
        var actual = _detector.DetectSingle(word);

        actual.Should().NotBeNull();
        actual.IsMatch.Should().BeTrue();
    }

    [Theory]
    [InlineData("что")]
    [InlineData("кто")]
    [InlineData("когда")]
    [InlineData("почему")]
    [InlineData("множество")]
    [InlineData("чего")]
    public void ShouldNot_Detect_WhenIsNotUncertain(string word)
    {
        var actual = _detector.DetectSingle(word);

        actual.Should().NotBeNull();
        actual.IsMatch.Should().BeFalse();
    }
}