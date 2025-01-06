using JuTCo.Core.Domain;
using JuTCo.Text.Review.Contracts;

namespace JuTCo.Text.Review.Extensions;

public static class MapperExtensions
{
    public static Fragment ToFragment(this DetectResult detectResult, int start = 0, int end = 0)
    {
        var style = new HintStyle()
        {
            Name = detectResult.Color!,
            Color = detectResult.Color!
        };
        var tab = new HintTab()
        {
            Name = detectResult.Tab!,
            Color = detectResult.Tab!
        };
        var hint = new Hint()
        {
            Name = detectResult.Name!,
            Description = detectResult.Description!,
            ShortDescription = detectResult.ShortDescription!,
            Weight = detectResult.Weight,
            Style = style,
            Tab = tab
        };
        return new Fragment()
        {
            Hint = hint,
            Start = detectResult.Start ?? start,
            End = detectResult.End ?? end
        };
    }
}