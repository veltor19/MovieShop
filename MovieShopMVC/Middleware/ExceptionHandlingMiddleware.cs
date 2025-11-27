using Serilog;
using System.Net;
using System.Text.Json;

namespace MovieShopMVC.Middleware {
    public class ExceptionHandlingMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            } catch (Exception ex) {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception) {
            var (statusCode, message) = exception switch {
                ArgumentNullException => (HttpStatusCode.BadRequest, "A required argument was null."),
                ArgumentException => (HttpStatusCode.BadRequest, exception.Message),
                KeyNotFoundException => (HttpStatusCode.NotFound, "The requested resource was not found"),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "You are not authorized to access this resource."),
                InvalidOperationException => (HttpStatusCode.BadRequest, exception.Message),
                NotImplementedException => (HttpStatusCode.NotImplemented, "The requested functionality is not implemented."),
                _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
            };

            // Log exception with structured data using Serilog
            Log.Error(exception,
                "Exception occurred: {ExceptionType} | StatusCode: {StatusCode} | Path: {Path} | Method: {Method} | UserId: {UserId}",
                exception.GetType().Name,
                (int)statusCode,
                context.Request.Path,
                context.Request.Method,
                context.Session.GetString("UserId") ?? "Anonymous"
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var errorResponse = new ErrorResponse {
                StatusCode = (int)statusCode,
                Message = message,
                Details = exception.Message,
                Timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            await context.Response.WriteAsync(json);
        }
    }
}