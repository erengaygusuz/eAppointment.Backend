using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.Json;
using TS.Result;

namespace eAppointment.Backend.WebAPI.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<object> _localization;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, IStringLocalizer<object> localization, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _localization = localization;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == 401)
                {
                    _logger.LogError(_localization["ErrorsCodes.401"].Value);

                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(401, _localization["ErrorsCodes.401"].Value));
                }

                else if(context.Response.StatusCode == 403)
                {
                    _logger.LogError(_localization["ErrorsCodes.403"].Value);

                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(403, _localization["ErrorsCodes.403"].Value));
                }
            }

            catch (Exception exception)
            {
                _logger.LogError(exception.Message);

                if (exception.GetType() == typeof(ValidationException))
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";

                    var errorResult = Result<string>.Failure(400, ((ValidationException)exception).Errors.Select(s => s.PropertyName).ToList());

                    await context.Response.WriteAsync(JsonSerializer.Serialize(errorResult));
                }

                else
                {
                    context.Response.StatusCode = 500;

                    await context.Response.WriteAsJsonAsync(Result<string>.Failure(500, _localization["ErrorsCodes.500"].Value));
                }
            }
        }
    }
}