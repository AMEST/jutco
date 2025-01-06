using FluentAssertions;
using JuTCo.Text.Review.Detectors;

namespace JuTCo.Text.Review.Tests;

public class ChancelleryDetectorTests
{
    private readonly ChancelleryDetector _detector = new();

    [Theory]
    [InlineData("апеллировать")]
    [InlineData("Катализатор")]
    [InlineData("миграция")]
    [InlineData("тривиальный")]
    [InlineData("тривиально")]
    public void Should_Detect_WhenIsCancellery(string word)
    {
        var actual = _detector.DetectSingle(word);

        actual.Should().NotBeNull();
        actual.IsMatch.Should().BeTrue();
    }

    [Theory]
    [InlineData("апелляция")]
    [InlineData("мигрировать")]
    [InlineData("множество")]
    [InlineData("чего")]
    public void ShouldNot_Detect_WhenIsNotCancellery
        (string word)
    {
        var actual = _detector.DetectSingle(word);

        actual.Should().NotBeNull();
        actual.IsMatch.Should().BeFalse();
    }
}