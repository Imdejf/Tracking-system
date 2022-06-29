using TrackingSystem.Shared.Abstract;
using TrackingSystem.Shared.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TrackingSystem.Shared.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BaseAppException baseAppException)
            {
                await throwError(context, baseAppException.StatusCodeToRise, baseAppException.Errors);
            }
            catch (Exception exception)
            {
                await throwError(context, 500, new Dictionary<string, string[]> { { "Message", new string[] { exception.Message } } });
            }
        }

        private Task throwError(HttpContext context, int statusCode, Dictionary<string, string[]> errors)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            return response.WriteAsync(JsonSerializer.Serialize(ApiResponse.Failure(statusCode, errors)));
        }
    }

}
