using JuTCo.Core.Services;

namespace JuTCo.Web.Beautifier.Contracts;

internal class BeautifierAppService : IBeautifierAppService
{
    private readonly IAITextProcessingService _textProcessingService;

    public BeautifierAppService(IAITextProcessingService textProcessingService)
    {
        _textProcessingService = textProcessingService;
    }
    
    public async Task<BeautifierResponse> TextCorrection(BeautifierRequest request)
    {
        var result = await _textProcessingService.TextCorrection(request.Text);
        return new BeautifierResponse()
        {
            Text = result
        };
    }

    public async Task<BeautifierResponse> Emojination(BeautifierRequest request)
    {
        var result = await _textProcessingService.TextEmojination(request.Text);
        return new BeautifierResponse()
        {
            Text = result
        };
    }

    public async Task<BeautifierResponse> Beautify(BeautifierRequest request)
    {
        var result = await _textProcessingService.TextBeautifier(request.Text, request.AdditionalPrompt);
        return new BeautifierResponse()
        {
            Text = result
        };
    }
}