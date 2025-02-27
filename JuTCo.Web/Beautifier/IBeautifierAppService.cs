using JuTCo.Web.Beautifier.Contracts;

namespace JuTCo.Web.Beautifier;

public interface IBeautifierAppService
{
    Task<BeautifierResponse> TextCorrection(BeautifierRequest request);

    Task<BeautifierResponse> Emojination(BeautifierRequest request);

    Task<BeautifierResponse> Beautify(BeautifierRequest request);
}