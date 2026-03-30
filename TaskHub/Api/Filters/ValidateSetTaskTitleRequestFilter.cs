using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ValidateSetTaskTitleRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("request", out var request) || request == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}