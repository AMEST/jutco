using System.ComponentModel.DataAnnotations;

namespace JuTCo.Web.Beautifier.Contracts;

public class BeautifierRequest
{
    [Required]
    public string Text { get; set; }
    
    public string? AdditionalPrompt { get; set; }
}