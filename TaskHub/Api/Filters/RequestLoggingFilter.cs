using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class RequestLoggingFilter : IActionFilter
    {
        private readonly ILogger<RequestLoggingFilter> _logger;
        private Stopwatch? _stopwatch;

        public RequestLoggingFilter(ILogger<RequestLoggingFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _stopwatch = Stopwatch.StartNew();
            var httpMethod = context.HttpContext.Request.Method;
            var path = context.HttpContext.Request.Path;
            
            _logger.LogInformation($"{httpMethod} {path}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (_stopwatch == null) return;
                
            _stopwatch.Stop();
            var statusCode = context.HttpContext.Response.StatusCode;
            var elapsedMs = _stopwatch.ElapsedMilliseconds;
            
            _logger.LogInformation($"{statusCode} {elapsedMs}ms");
        }
    }
}