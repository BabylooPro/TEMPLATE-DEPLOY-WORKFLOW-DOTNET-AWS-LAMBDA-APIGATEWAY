using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WeatherForecast.Middleware
{
    public class EnvLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public EnvLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // LOG ENVIRONMENT VARIABLES FOR EACH REQUEST
            Program.LogEnvironmentVariables();

            // CALL NEXT DELEGATE/MIDDLEWARE IN PIPELINE
            await _next(context);
        }
    }

    // EXTENSION METHOD TO MAKE MIDDLEWARE REGISTRATION CLEANER
    public static class EnvLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseEnvLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EnvLoggingMiddleware>();
        }
    }
} 
