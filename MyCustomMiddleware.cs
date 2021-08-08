using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DotNetMiddleware
{
    public class MyCustomMiddleware
    {
        RequestDelegate _next;
        ILoggerFactory _loggerFactory;
        IOptions<MyCustomMiddlewareOptions> _options;

        public MyCustomMiddleware(RequestDelegate next, ILoggerFactory loggerFactory, IOptions<MyCustomMiddlewareOptions> options)
        {
            _next = next;
            _loggerFactory = loggerFactory;
            _options = options;
        }

        public async Task Invoke(HttpContext context)
        {
            //_loggerFactory.AddConsole();
            //var logger = _loggerFactory.CreateLogger("My own logger..");
            //logger.LogInformation("My middleware class is handling the request");

            //await context.Response.WriteAsync("My middleware class is handling the request");
            await context.Response.WriteAsync(_options.Value.OptionOne + "<br>");

            await _next.Invoke(context);
            await context.Response.WriteAsync("My middleware class has completed handling the request");
        }
    }

    public class MyCustomMiddlewareOptions
    {
        public string OptionOne { get; set; }
    }

    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyCustomMiddleware>();
        }
    }
}