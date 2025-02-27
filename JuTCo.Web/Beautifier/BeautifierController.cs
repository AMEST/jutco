using JuTCo.Web.Beautifier.Contracts;
using JuTCo.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace JuTCo.Web.Beautifier;

[ApiController]
[HandleError]
[Route("api/[controller]")]
public class BeautifierController  : ControllerBase
{
    private readonly IBeautifierAppService _beautifierAppService;

    public BeautifierController(IBeautifierAppService beautifierAppService)
    {
        _beautifierAppService = beautifierAppService;
    }
    
    [HttpPost]
    [ProducesResponseType<BeautifierResponse>(200)]
    [ValidateModel]
    public async Task<IActionResult> Beautifier([FromBody] BeautifierRequest request)
    {
        return Ok(await _beautifierAppService.Beautify(request));
    }
    
    [HttpPost("[action]")]
    [ProducesResponseType<BeautifierResponse>(200)]
    [ValidateModel]
    public async Task<IActionResult> TextCorrection([FromBody] BeautifierRequest request)
    {
        return Ok(await _beautifierAppService.TextCorrection(request));
    }
    
    [HttpPost("[action]")]
    [ProducesResponseType<BeautifierResponse>(200)]
    [ValidateModel]
    public async Task<IActionResult> Emojination([FromBody] BeautifierRequest request)
    {
        return Ok(await _beautifierAppService.Emojination(request));
    }
}