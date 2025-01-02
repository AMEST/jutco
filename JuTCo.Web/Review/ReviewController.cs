using JuTCo.Web.Review.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace JuTCo.Web.Review;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType<ReviewResultModel>(200)]
    [ProducesResponseType<BadReviewResultModel>(400)]
    [ProducesResponseType<BadReviewResultModel>(500)]
    public async Task<IActionResult> Review([FromBody] ReviewRequestModel request)
    {
        return Ok();
    }
}