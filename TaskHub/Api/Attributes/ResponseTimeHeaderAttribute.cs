using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attributes;

public class ResponseTimeHeaderAttribute : ActionFilterAttribute
{
    private readonly Stopwatch timer = new();

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        timer.Start();
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        timer.Stop();

        if (!context.HttpContext.Response.HasStarted)
        {
            context.HttpContext.Response.Headers.Append("X-Response-Time-Ms", timer.ElapsedMilliseconds.ToString());
        }
    }
}