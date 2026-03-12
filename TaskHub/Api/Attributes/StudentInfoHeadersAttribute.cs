using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class StudentInfoHeadersAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (!context.HttpContext.Response.HasStarted)
        {
            context.HttpContext.Response.Headers.Append("X-Student-Name", "Kolmykova Aleksandra Dmitrievna");
            context.HttpContext.Response.Headers.Append("X-Student-Group", "RI-240932");
        }
    }
}