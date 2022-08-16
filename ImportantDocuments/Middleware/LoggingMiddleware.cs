using Microsoft.AspNetCore.Http.Extensions;

namespace ImportantDocuments.API.Middleware;

public class LoggingMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly RequestDelegate _next;
 
    public LoggingMiddleware(ILogger<LoggingMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }
 
    public async Task Invoke(HttpContext context)
    {
        _logger.LogInformation($"About to start {context.Request.Method} {context.Request.GetDisplayUrl()} request");
 
        await _next(context);
 
        _logger.LogInformation($"Request completed with status code: {context.Response.StatusCode} ");
    }
}