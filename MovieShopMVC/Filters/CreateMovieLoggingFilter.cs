using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using System.Text;

namespace MovieShopMVC.Filters {
    public class CreateMovieLoggingFilter : IActionFilter {
        public void OnActionExecuting(ActionExecutingContext context) {
            var request = context.HttpContext.Request;
            var userId = context.HttpContext.Session.GetString("UserId");
            var userName = context.HttpContext.Session.GetString("UserName");
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress?.ToString();

            // Log with structured data using Serilog
            Log.Information(
                "CreateMovie Action Executing | Method: {Method} | Path: {Path} | UserId: {UserId} | UserName: {UserName} | IP: {IpAddress} | QueryString: {QueryString}",
                request.Method,
                request.Path,
                userId ?? "Not authenticated",
                userName ?? "N/A",
                ipAddress ?? "Unknown",
                request.QueryString.ToString()
            );

            // Log action arguments if any
            if (context.ActionArguments.Any()) {
                foreach (var arg in context.ActionArguments) {
                    Log.Information(
                        "CreateMovie Action Argument | Key: {ArgumentKey} | Type: {ArgumentType}",
                        arg.Key,
                        arg.Value?.GetType().Name ?? "null"
                    );
                }
            }

            // Console output for visibility (optional)
            Console.WriteLine("🎬🎬🎬 FILTER EXECUTING 🎬🎬🎬");
            Console.WriteLine($"Method: {request.Method} | Path: {request.Path}");
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            var statusCode = context.HttpContext.Response.StatusCode;
            var resultType = context.Result?.GetType().Name ?? "No Result";

            // Log execution result with structured data
            Log.Information(
                "CreateMovie Action Executed | StatusCode: {StatusCode} | ResultType: {ResultType} | HasException: {HasException}",
                statusCode,
                resultType,
                context.Exception != null
            );

            // Log exception if present
            if (context.Exception != null) {
                Log.Error(
                    context.Exception,
                    "CreateMovie Action Exception | ExceptionType: {ExceptionType}",
                    context.Exception.GetType().Name
                );
            }

            // Console output for visibility (optional)
            Console.WriteLine("✅✅✅ FILTER EXECUTED ✅✅✅");
            Console.WriteLine($"Status: {statusCode} | Result: {resultType}");
        }
    }
}
