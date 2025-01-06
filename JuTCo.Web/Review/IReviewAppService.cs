using JuTCo.Web.Review.Contracts;

namespace JuTCo.Web.Review;

public interface IReviewAppService
{
    public Task<ReviewResultModel> Review(ReviewRequestModel request);
}