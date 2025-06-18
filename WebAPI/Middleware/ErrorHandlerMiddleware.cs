using System.Net;
using Microsoft.AspNetCore.Http;

namespace Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            switch (exception)
            {
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized; // 401
                    break;
                case BadHttpRequestException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;
                case ArgumentException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;
                default:
                    status = HttpStatusCode.InternalServerError; // 500
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsJsonAsync(new { error = message });
        }
    }
}
