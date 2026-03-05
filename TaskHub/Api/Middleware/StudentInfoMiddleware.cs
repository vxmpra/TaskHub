namespace Api.Middleware;

public class StudentInfoMiddleware
{
    private readonly RequestDelegate _nextMiddleware;

    public StudentInfoMiddleware(RequestDelegate nextMiddleware)
    {
        _nextMiddleware = nextMiddleware;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.ContainsKey("X-Student-Name"))
            {
                httpContext.Response.Headers.Append("X-Student-Name", "Kolmykova Aleksandra Dmitrievna");
            }
            
            if (!httpContext.Response.Headers.ContainsKey("X-Student-Group"))
            {
                httpContext.Response.Headers.Append("X-Student-Group", "RI-240932");
            }
            
            return Task.CompletedTask;
        });

        await _nextMiddleware(httpContext);
    }
}