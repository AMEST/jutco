using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Net.Http.Headers;

namespace JuTCo.Web.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;

            var keyValuePairs = context.ModelState.Where(x => x.Value.Errors.Any());
            var errors = keyValuePairs
                .Select(item => 
                    new ValidationError(item.Key, string.Join("; ", item.Value.Errors.Select(x => x.ErrorMessage))))
                .ToList();
            
            var errorResult = new {Errors = errors};

            var result = new ObjectResult(errorResult) {StatusCode = (int) HttpStatusCode.BadRequest};
            result.ContentTypes.Add(new MediaTypeHeaderValue("application/json"));
            context.Result = result;
        }
    }
}