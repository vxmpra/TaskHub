using Api.Controllers.Tasks.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class ValidateCreateTaskRequestFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ActionArguments.TryGetValue("request", out var requestObj) || requestObj == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            var request = requestObj as CreateTaskRequest;
            
            if (request == null)
            {
                context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
                return;
            }

            if (request.CreatedByUserId == Guid.Empty)
            {
                context.Result = new BadRequestObjectResult("Идентификатор пользователя не задан");
                return;
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                context.Result = new BadRequestObjectResult("Название задачи не задано");
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}