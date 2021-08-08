using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace DotNetMiddleware
{
    public class MyHandlerMiddleware
    {
        RequestDelegate _next;

        public MyHandlerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("</br><h3>Your weekly timetable...</h3></br></br>");
        }
    }

    public static class MyHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyHandlerMiddleware>();
        }
    }
}
