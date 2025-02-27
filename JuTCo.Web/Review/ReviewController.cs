using JuTCo.Web.Filters;
using JuTCo.Web.Review.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JuTCo.Web.Review;

[ApiController]
[Route("api/[controller]")]
[HandleReviewError]
public class ReviewController : ControllerBase
{
    private readonly IReviewAppService _reviewAppService;

    public ReviewController(IReviewAppService reviewAppService)
    {
        _reviewAppService = reviewAppService;
    }
    
    [HttpPost]
    [ValidateModel]
    [ProducesResponseType<ReviewResultModel>(200)]
    [ProducesResponseType<BadReviewResultModel>(400)]
    [ProducesResponseType<BadReviewResultModel>(500)]
    public async Task<IActionResult> Review([FromBody] ReviewRequestModel request)
    {
        var result = await _reviewAppService.Review(request);
        return Ok(result);
    }
}