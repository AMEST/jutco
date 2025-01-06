using JuTCo.Web.Exceptions;
using JuTCo.Web.Review.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JuTCo.Web.Filters;

public class HandleReviewErrorAttribute : ExceptionFilterAttribute
{
    /// <inheritdoc />
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ArgumentOutOfRangeException notFoundEx:
                context.Result = new NotFoundObjectResult(new BadReviewResultModel()
                {
                    Code = "404",
                    Message = notFoundEx.Message
                });
                context.ExceptionHandled = true;
                break;
            case BadRequestException badRequestException:
                context.Result = new BadRequestObjectResult(new BadReviewResultModel()
                {
                    Code = "400",
                    Message = badRequestException.Message
                });
                context.ExceptionHandled = true;
                break;
            case InvalidOperationException invalidOperationException:
                
                context.Result = new BadRequestObjectResult(new BadReviewResultModel()
                    {
                        Code = "400",
                        Message = invalidOperationException.Message
                    });
                context.ExceptionHandled = true;
                break;
            default:
                context.Result = new ObjectResult(new BadReviewResultModel()
                {
                    Code = "500",
                    Message = context.Exception.Message
                });
                break;
        }

        return base.OnExceptionAsync(context);
    }
}