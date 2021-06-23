
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace New_folder__2_.Middlewaer
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

            public CustomMiddleware(RequestDelegate next)
            {
                _next = next;
            }

            public async Task InvokeAsync(HttpContext context, IHostingEnvironment env)
            {
                //await context.Response.WriteAsync($"Hello");
                
                string filePath = Path.Combine(env.WebRootPath, $"Data/data.txt");
                var file = System.IO.File.Create(filePath);
                var write = new System.IO.StreamWriter(file);
               

               var scheme = context.Request.Scheme;
               write.WriteLine($"Scheme:{scheme}");
                var host = context.Request.Host;
                write.WriteLine($"Host: {host}");
                var path = context.Request.Path;
                write.WriteLine($"Path: {path}");
                var query = context.Request.QueryString;
                write.WriteLine($"QueryString :{query}");
                var body = context.Request.Body;
                write.WriteLine($"Body: {body}");
                
                
                write.Dispose();

                
                await _next(context);

            }
    }
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}