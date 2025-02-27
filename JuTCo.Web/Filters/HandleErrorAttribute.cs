using JuTCo.Web.Exceptions;
using JuTCo.Web.Review.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JuTCo.Web.Filters;

public class HandleErrorAttribute : ExceptionFilterAttribute
{
    private static readonly string[] PropertiesBlackList =
        new[] { "message", "data", "hresult", "source", "stacktrace", "targetsite", "innerexception", "helplink", "serializationstacktracestring" };
    
    /// <inheritdoc />
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        var problemDetails = new ProblemDetails()
        {
            Detail = context.Exception.Message
        };
        FillExtensions(problemDetails, context.Exception);
        switch (context.Exception)
        {
            case ArgumentOutOfRangeException:
                problemDetails.Status = 404;
                context.Result = new NotFoundObjectResult(problemDetails);
                context.ExceptionHandled = true;
                break;
            case BadRequestException:
                problemDetails.Status = 400;
                context.Result = new BadRequestObjectResult(problemDetails);
                context.ExceptionHandled = true;
                break;
            case InvalidOperationException:
                problemDetails.Status = 400;
                context.Result = new BadRequestObjectResult(problemDetails);
                context.ExceptionHandled = true;
                break;
            default:
                problemDetails.Status = 500;
                context.Result = new ObjectResult(problemDetails);
                break;
        }

        return base.OnExceptionAsync(context);
    }

    private static void FillExtensions(ProblemDetails details, Exception e)
    {
        foreach(var prop in e.GetType().GetProperties())
        {
            var name = prop.Name;
            if(PropertiesBlackList.Contains(name.ToLower()))
                continue;
            if(details.Extensions.ContainsKey(name))
                continue;
                
            var value = prop.GetValue(e, null);
            if(value is null)
                continue;
                
            if(value is string valueString && string.IsNullOrEmpty(valueString))
                continue;
                
            details.Extensions.Add(name, value);
        } 
    }
}