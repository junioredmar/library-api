using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Library.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory
                .CreateLogger<ErrorHandlingMiddleware>();
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex, env, _logger);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostingEnvironment env, ILogger logger)
        {
            logger.LogError(ex.Message, ex);

            var result = JsonConvert.SerializeObject(new
            {
                title = "One or more unexpected errors occurred.",
                status = (int)HttpStatusCode.InternalServerError,
                exception = env.IsProduction() ? null : ex
            });

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(result);
        }
    }
}
