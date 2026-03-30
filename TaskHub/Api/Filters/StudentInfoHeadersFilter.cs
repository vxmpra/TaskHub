using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class StudentInfoHeadersFilter : IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context) 
        { 
            context.HttpContext.Response.Headers.Append("X-Student-Name", "Kolmykova Aleksandra Dmitrievna");
            context.HttpContext.Response.Headers.Append("X-Student-Group", "RI-240932");
        }

         public void OnActionExecuting(ActionExecutingContext context) { }
    }
}