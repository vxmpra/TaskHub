using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class ValidateUserRequestAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.ActionArguments.Values.FirstOrDefault();
        
        if (request == null)
        {
            context.Result = new BadRequestObjectResult("Тело запроса отсутствует");
            return;
        }

        var nameValue = request.GetType().GetProperty("Name")?.GetValue(request) as string;
        
        if (string.IsNullOrWhiteSpace(nameValue))
        {
            context.Result = new BadRequestObjectResult("Имя пользователя не задано");
            return;
        }
    }
}