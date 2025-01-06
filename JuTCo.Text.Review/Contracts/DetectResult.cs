namespace JuTCo.Text.Review.Contracts;

public class DetectResult
{
    public static readonly DetectResult NotMatch = new DetectResult() { IsMatch = false };
        
    public bool IsMatch { get; private set; } = true;

    public string? Name { get; set; }

    public string? Color { get; set; }
    
    public string? Tab { get; set; }

    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public int Weight { get; set; } = 10;
    
    public int? Start { get; set; }
    
    public int? End { get; set; }
}