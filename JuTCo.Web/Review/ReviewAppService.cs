using JuTCo.Core.Services;
using JuTCo.Web.Exceptions;
using JuTCo.Web.Mapping;
using JuTCo.Web.Review.Contracts;

namespace JuTCo.Web.Review;

public class ReviewAppService : IReviewAppService
{
    private readonly ITextReviewService _textReviewService;

    public ReviewAppService(ITextReviewService textReviewService)
    {
        _textReviewService = textReviewService;
    }
    
    public async Task<ReviewResultModel> Review(ReviewRequestModel request)
    {
        if (string.IsNullOrWhiteSpace(request?.Text))
            throw new BadRequestException("Получен пустой текст, невозможно его обработать");

        var review = await _textReviewService.Review(request.Text);
        review.Fragments = review.Fragments.OrderByDescending(x => x.Hint.Weight).ToArray();
        return review.ToModel();
    }
}