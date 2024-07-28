using eAppointment.Backend.WebAPI.Concretes;
using System.Net;
using System.Text.Json;
using TS.Result;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
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
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = exception switch
            {
                KeyNotFoundException => HttpStatusCode.NotFound,
                ArgumentException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            response.StatusCode = (int)statusCode;

            var errorDetails = new ErrorDetails
            {
                StatusCode = response.StatusCode,
                Message = exception.Message,
                Detailed = exception.StackTrace
            };

            var result = Result<ErrorDetails>.Failure(errorDetails.Message);
            var errorJson = JsonSerializer.Serialize(result);

            return response.WriteAsync(errorJson);
        }
    }
}
