using eAppointment.Backend.WebAPI.Concretes;
using Microsoft.Extensions.Localization;
using System.Text.Json;
using TS.Result;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<object> _localization;

        public ErrorHandlerMiddleware(RequestDelegate next, IStringLocalizer<object> localization)
        {
            _next = next;
            _localization = localization;
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = response.StatusCode;

            var errorMessage = statusCode switch
            {
                400 => _localization["ErrorsCodes.400"].Value,
                401 => _localization["ErrorsCodes.401"].Value,
                403 => _localization["ErrorsCodes.403"].Value,
                404 => _localization["ErrorsCodes.404"].Value,
                405 => _localization["ErrorsCodes.405"].Value,
                408 => _localization["ErrorsCodes.408"].Value,
                410 => _localization["ErrorsCodes.410"].Value,
                413 => _localization["ErrorsCodes.413"].Value,
                414 => _localization["ErrorsCodes.414"].Value,
                415 => _localization["ErrorsCodes.415"].Value,
                429 => _localization["ErrorsCodes.429"].Value,
                431 => _localization["ErrorsCodes.431"].Value,
                500 => _localization["ErrorsCodes.500"].Value,
                502 => _localization["ErrorsCodes.502"].Value,
                503 => _localization["ErrorsCodes.503"].Value,
                504 => _localization["ErrorsCodes.504"].Value,
                _ => _localization["ErrorsCodes.Unknown"].Value
            };

            var errorDetails = new ErrorDetails
            {
                StatusCode = statusCode,
                Message = errorMessage,
                Detailed = exception.StackTrace
            };

            var result = Result<ErrorDetails>.Failure(errorDetails.Message);
            var errorJson = JsonSerializer.Serialize(result);

            return response.WriteAsync(errorJson);
        }
    }
}
