using System.Diagnostics;

namespace Api.Middleware 
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _nextMiddleware;

        public ResponseTimeMiddleware(RequestDelegate nextMiddleware)
        {
            _nextMiddleware = nextMiddleware;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var timer = Stopwatch.StartNew();

            httpContext.Response.OnStarting(() =>
            {
                timer.Stop();
                httpContext.Response.Headers["X-Response-Time-Ms"] = timer.ElapsedMilliseconds.ToString();
                return Task.CompletedTask;
            });

            await _nextMiddleware(httpContext);
        }
    }
}