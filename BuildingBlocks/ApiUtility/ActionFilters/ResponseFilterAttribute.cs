
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedModel;

namespace ApiUtility.ActionFilters
{
    public class ResponseFilterAttribute<T> : IActionFilter
    {
        /// <summary>
        /// Set the NoContentResult if response object has not data or pagedResult.Result has no data. 
        /// </summary>
        /// <param name="context">the context.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null || !context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                return;
            }

            var objectResult = context.Result as ObjectResult;

            if (objectResult?.Value is T)
                return;

            if ((objectResult?.Value is IEnumerable<T> result) && result.Any())
                return;

            if ((objectResult?.Value is PagedResults<T> pagedResult) && pagedResult.Results.Any())
                return;

            if (context.HttpContext.Request.Method != HttpMethod.Post.Method
                        && context.HttpContext.Request.Method != HttpMethod.Put.Method)
                context.Result = new NoContentResult();
        }

        /// <summary>
        /// On Action Executing
        /// </summary>
        /// <param name="context">the context.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // Do nothing On Action Executing
        }
    }
}
