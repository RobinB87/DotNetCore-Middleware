using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetMiddleware
{
    public class MyCustomMiddleware
    {
        RequestDelegate _next;
        ILoggerFactory _loggerFactory;

        public MyCustomMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _loggerFactory = loggerFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            _loggerFactory.AddConsole();
            var logger = _loggerFactory.CreateLogger("My own logger..");
            logger.LogInformation("My middleware class is handling the request");

            await context.Response.WriteAsync("My middleware class is handling the request");
            await _next.Invoke(context);
            await context.Response.WriteAsync("My middleware class has completed handling the request");
        }
    }

    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}