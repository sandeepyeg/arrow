namespace ArrowDrivingSchool.API.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path;
        var ip = context.Connection.RemoteIpAddress?.ToString();

        logger.LogInformation("[Request] {Method} {Path} from {IP}", method, path, ip);
        await next(context);
    }
}